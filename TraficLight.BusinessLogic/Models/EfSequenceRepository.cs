using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraficLight.BusinessLogic.Entities;
using Newtonsoft.Json;

namespace TraficLight.BusinessLogic.Models
{
    public class EfSequenceRepository : ISequenceRepository
    {
        protected TraficContext db;

        public EfSequenceRepository(DbContextOptions options)
        {
            db = new TraficContext(options);
        }

        public async Task<Answer> Create()
        {
            try
            {
                var guid = Guid.NewGuid().ToString();
                await db.Sequences.AddAsync(new Sequence { Id = guid });
                await db.SaveChangesAsync();

                return new Answer
                {
                    Status = "ok",
                    Responce = new Responce { Sequence = guid }
                };
            }
            catch (Exception exe)
            {
                return new Answer
                {
                    Status = "error",
                    Msg = exe.Message
                };
            }
        }

        public async Task<Answer> Add(Request request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Sequence) || request.Observation == null
                    || string.IsNullOrEmpty(request.Observation.Color)
                    || !(request.Observation.Color == "green" || request.Observation.Color == "red"))
                    throw new Exception("No solutions found");

                var sequense = await db.Sequences.FirstOrDefaultAsync(x => x.Id == request.Sequence); // try to find sequence

                if (sequense == null)
                    throw new Exception("The sequence isn't found");


                if (request.Observation.Color == "red")
                {
                    sequense = ClockFace.Update(sequense, 0, 0, true);
                }

                if (request.Observation.Color == "green")
                {
                    sequense = ClockFace.Update(sequense,
                        Convert.ToInt32(request.Observation.Numbers[0], 2),
                        Convert.ToInt32(request.Observation.Numbers[1], 2));

                    if (sequense.Broken)
                        throw new Exception("No solutions found");

                    db.Entry(sequense).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return new Answer
                {
                    Status = "ok",
                    Responce = new Responce
                    {
                        Start = JsonConvert.DeserializeObject<List<NumInfo>>(sequense.Start).Select(x => x.Start).ToArray(),
                        Missing = new string[] { Convert.ToString(sequense.FirstMissing, 2).PadLeft(7, '0'), Convert.ToString(sequense.SecondMissing, 2).PadLeft(7, '0') }
                    }
                };
            }
            catch (Exception exe)
            {
                return new Answer
                {
                    Status = "error",
                    Msg = exe.Message
                };
            }
        }

        public async Task<Answer> Clear()
        {
            try
            {
                db.RemoveRange(db.Sequences);
                await db.SaveChangesAsync();

                return new Answer
                {
                    Status = "ok",
                    Responce = "ok"
                };
            }
            catch (Exception exe)
            {
                return new Answer
                {
                    Status = "error",
                    Msg = exe.Message
                };
            }
        }

        #region Disposable
        bool _disposed;

        public void Dispose() // Из Интернета
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }

            }
            _disposed = true;
        }
        #endregion
    }
}
