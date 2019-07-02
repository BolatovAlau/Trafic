using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
        public Answer Create()
        {
            try
            {
                var guid = Guid.NewGuid();
                db.Sequences.Add(new Sequence { Id = guid });
                db.SaveChanges();

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

        public Answer Add(Request request)
        {
            try
            {
                db.RemoveRange(db.Sequences);
                db.SaveChanges();

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

        public Answer Clear()
        {
            try
            {
                db.RemoveRange(db.Sequences);
                db.SaveChanges();

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
