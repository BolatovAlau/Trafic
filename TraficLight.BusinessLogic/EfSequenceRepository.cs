using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraficLight.BusinessLogic.Entities;

namespace TraficLight.BusinessLogic
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

                if (request.Observation.Color == "green")
                {

                }
                else
                {
                    if (!sequense.IsNotFirst)
                        throw new Exception("There isn't enough data");
                    else
                    {
                        return new Answer
                        {
                            Status = "ok",
                            Responce = new Responce
                            {
                                //Start = sequense.Start,
                                //Missing = sequense.Missing
                            }
                        };
                    }
                }
                return null;
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

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
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
