using System;

namespace Business_Logic_Layer.DTO
{
    public class AnimeFigureDTO
    {
        private Guid id;

        private string animeFiguresName;

        private string animeFiguresPictureUrl;

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        public string AnimeFiguresName
        {
            get { return animeFiguresName; }
            set { animeFiguresName = value; }
        }
        public string AnimeFiguresPictureUrl {
            get { return animeFiguresPictureUrl; }
        }
        public AnimeFigureDTO()
        {   
        }

        public AnimeFigureDTO(string name , string url)
        {
            animeFiguresName =name;
            animeFiguresPictureUrl =url;
        }
    }
}