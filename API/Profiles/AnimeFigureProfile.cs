using API.Models;
using AutoMapper;
using Business_Logic_Layer.DTO;
using Data_Access_Layer.Entities;

namespace API.Profiles
{
    public class AnimeFigureProfile : Profile
    {
        public AnimeFigureProfile()
        {
            CreateMap<AnimeFigureDTO, AnimeFigure>()
                .ForMember(
                    dest => dest.animeFiguresName,
                    from => from.MapFrom(x => x.AnimeFiguresName)
                )
                .ForMember(
                    dest => dest.animeFiguresPictureUrl,
                    from => from.MapFrom(x => x.AnimeFiguresPictureUrl)
                );
            CreateMap<AnimeFigure, AnimeFigureDTO>()
                .ForMember(
                    dest => dest.AnimeFiguresName,
                    from => from.MapFrom(x => x.animeFiguresName)
                )
                .ForMember(
                    dest => dest.AnimeFiguresPictureUrl,
                    from => from.MapFrom(x => x.animeFiguresPictureUrl)
                )
                .ForMember(
                    dest => dest.Id,
                    from => from.MapFrom(x => x.id)
                );
            CreateMap<AnimeFigureDTO, AnimeFigureViewModel>()
                .ForMember(
                    dest => dest.AnimeFiguresName,
                    from => from.MapFrom(x => x.AnimeFiguresName)
                )
                .ForMember(
                    dest => dest.AnimeFiguresPictureUrl,
                    from => from.MapFrom(x => x.AnimeFiguresPictureUrl)
                )
                .ForMember(
                    dest => dest.Id,
                    from => from.MapFrom(x => x.Id)
                );
            CreateMap<AnimeFigureViewModel, AnimeFigure>()
                .ForMember(
                    dest => dest.animeFiguresName,
                    from => from.MapFrom(x => x.AnimeFiguresName)
                )
                .ForMember(
                    dest => dest.animeFiguresPictureUrl,
                    from => from.MapFrom(x => x.AnimeFiguresPictureUrl)
                );
            CreateMap<AnimeFigureViewModel, AnimeFigureDTO>()
                .ForMember(
                    dest => dest.AnimeFiguresName,
                    from => from.MapFrom(x => x.AnimeFiguresName)
                )
                .ForMember(
                    dest => dest.AnimeFiguresPictureUrl,
                    from => from.MapFrom(x => x.AnimeFiguresPictureUrl)
                );
            CreateMap<AnimeFigure, AnimeFigureViewModel>()
                .ForMember(
                    dest => dest.AnimeFiguresName,
                    from => from.MapFrom(x => x.animeFiguresName)
                )
                .ForMember(
                    dest => dest.AnimeFiguresPictureUrl,
                    from => from.MapFrom(x => x.animeFiguresPictureUrl)
                )
                .ForMember(
                    dest => dest.Id,
                    from => from.MapFrom(x => x.id)
                );
        }
    }
}
