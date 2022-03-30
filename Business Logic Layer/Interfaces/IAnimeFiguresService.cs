using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;

namespace NLayerApp.BLL.Interfaces
{
    public interface IAnimeFiguresService
    {
        List<AnimeFigure> GetAnimeFigures();
        AnimeFigure GetAnimeFigure(Guid id);
        void AddAnimeFigure(AnimeFigure animeFigure);
        bool UpdateAnimeFigure(AnimeFigure animeFigure);
        bool DeleteAnimeFigure(Guid id);
        void Dispose();
    }
}