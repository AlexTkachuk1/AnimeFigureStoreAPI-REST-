using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.DTO;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using NLayerApp.BLL.Interfaces;

namespace Business_Logic_Layer.Services
{
    public class AnimeFiguresService : IAnimeFiguresService
    {
        public IUnitOfWork Database;
        public AnimeFiguresService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public List<AnimeFigure> GetAnimeFigures()
        {
            List<AnimeFigure> animeFigures = Database.AnimeFigures.GetAllAsync().Result;

            return animeFigures;
        }
        public AnimeFigure GetAnimeFigure(Guid id)
        {
            var animeFigures = Database.AnimeFigures.GetAsync(id).Result; 
            return animeFigures;
        }
        public void AddAnimeFigure(AnimeFigure animeFigure)
        {
            Database.AnimeFigures.CreateOrUpdateAsync(animeFigure).Wait();
        }
        public bool UpdateAnimeFigure(AnimeFigure animeFigure)
        {
            var answer = Database.AnimeFigures.CreateOrUpdateAsync(animeFigure).Result;
            return answer;
        }
        public bool DeleteAnimeFigure(Guid id)
        {
            var answer = Database.AnimeFigures.DeleteAsync(id).Result;
            return answer;
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}