using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using System;

namespace Data_Access_Layer.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private AnimeFiguresRepository animeFiguresRepository;

        public EFUnitOfWork()
        {
        }

        public IRepository<AnimeFigure> AnimeFigures
        {
            get
            {
                if (animeFiguresRepository == null)
                    animeFiguresRepository = new AnimeFiguresRepository();
                return animeFiguresRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
