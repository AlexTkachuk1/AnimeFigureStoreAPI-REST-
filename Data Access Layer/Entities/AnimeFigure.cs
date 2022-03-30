using System;
using System.Text.RegularExpressions;

namespace Data_Access_Layer.Entities
{
    public class AnimeFigure
    {
        public Guid id { get; }
        public string animeFiguresName { get; }
        public string animeFiguresPictureUrl { get; }

        public AnimeFigure(string animeFiguresName, string animeFiguresPictureUrl)
        {
            this.animeFiguresName = animeFiguresName;
            this.animeFiguresPictureUrl = animeFiguresPictureUrl;
            id = Guid.NewGuid();
        }
        public AnimeFigure(string animeFiguresName, string animeFiguresPictureUrl, string animeFiguresId)
        {
            this.animeFiguresName = animeFiguresName;
            this.animeFiguresPictureUrl = animeFiguresPictureUrl;
            id = StringToGuid(animeFiguresId);
        }

        // Добавлять логику в сущности на DAL уровне практика плохая(как мне кажется),
        // но поскольку в приложении не используеся база данных я посчитал это приемлимым.
        private Guid StringToGuid(string id)
        {
            var reGuid = new Regex(
            @"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$",
            RegexOptions.Compiled);

            if (id == null || id.Length != 36) return Guid.Empty;
            if (reGuid.IsMatch(id))
                return new Guid(id);
            else
                return Guid.Empty;
        }
    }
}
