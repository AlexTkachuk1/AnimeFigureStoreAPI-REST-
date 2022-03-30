using System;

namespace API.Models
{
    internal class AnimeFigureViewModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string AnimeFiguresName { get; set; }
        public string AnimeFiguresPictureUrl { get; set; }
    }
}
