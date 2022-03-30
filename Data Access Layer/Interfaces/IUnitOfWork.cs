using Data_Access_Layer.Entities;
using System;

namespace Data_Access_Layer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AnimeFigure> AnimeFigures { get; }
    }
}
