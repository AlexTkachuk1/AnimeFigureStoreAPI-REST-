using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Data_Access_Layer.Repositories
{
    public class AnimeFiguresRepository: IRepository<AnimeFigure>
    {
        const string path = @"..\Data Access Layer\config.json";

        public static async Task<List<AnimeFigure>> Get_serverAsync()
        {
            var dataString = string.Empty;
            using (StreamReader sourceStream = new StreamReader(path))
            {
                dataString = await sourceStream.ReadToEndAsync();
            }

            var x = "||".ToCharArray();
            var splitStrings = dataString.Split(x, StringSplitOptions.RemoveEmptyEntries).ToList();

            var Figures = new List<AnimeFigure>();

            if (splitStrings.Count>3)
            {
                for (int i = 0; i < splitStrings.Count; i++)
                {
                    if (splitStrings[i].Contains("\""))
                    {
                        splitStrings.Remove(splitStrings[i]);
                    }
                }
                
                for (int i = 0; i < splitStrings.Count; i += 3)
                {
                    var Figure = new AnimeFigure(splitStrings[i + 1], splitStrings[i + 2], splitStrings[i]);
                    Figures.Add(Figure);
                }
            }
            return Figures;
        }
        public static async Task AddRecord_serverAsync(AnimeFigure animeFigure)
        {
            string stringForRecord = $"||{animeFigure.id}||{animeFigure.animeFiguresName}||{animeFigure.animeFiguresPictureUrl}||";
            var json = JsonConvert.SerializeObject(stringForRecord);
            using (StreamWriter sourceStream = new StreamWriter(path, true))
            {
                await sourceStream.WriteAsync(json);
            }   
        }
        public static async Task SetAllData_serverAsync(List<AnimeFigure> animeFigures)
        {
            string stringForRecord = string.Empty;

            foreach (var figure in animeFigures)
            {
                stringForRecord += $"||{figure.id}||{figure.animeFiguresName}||{figure.animeFiguresPictureUrl}||";
            }
            var json = JsonConvert.SerializeObject(stringForRecord);
            using (StreamWriter sourceStream = new StreamWriter(path, false))
            {
                await sourceStream.WriteAsync(json);
            }
        }

        public async Task<List<AnimeFigure>> GetAllAsync()
        {
            List<AnimeFigure> animeFigures = Get_serverAsync().Result;
            
            return animeFigures;
        }
        public async Task<AnimeFigure> GetAsync(Guid id)
        {
            List<AnimeFigure> animeFigures = Get_serverAsync().Result;

            AnimeFigure animeFigure = null;

            foreach (var figure in animeFigures)
            {
                if (figure.id == id)
                {
                    animeFigure = figure;
                }
            }
            return animeFigure;
        }
        public async Task<bool> CreateOrUpdateAsync(AnimeFigure data)
        {
            DeleteAsync(data.id);

            List<AnimeFigure> animeFigures = Get_serverAsync().Result;

            animeFigures.Add(data);

            SetAllData_serverAsync(animeFigures);

            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            List<AnimeFigure> animeFigures = GetAllAsync().Result;
            for (int i = 0; i < animeFigures.Count; i++)
            {
                if (animeFigures[i].id == id)
                {
                    animeFigures.Remove(animeFigures[i]);
                }
            }
            SetAllData_serverAsync(animeFigures);
            return true;
        }


    }
}