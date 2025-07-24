using AutoMapper;
using Store.DataAccess.Entities;
using Store.Services.Models;
using Store.WebAPI.DTOs;

namespace Store.WebAPI.Mapper
{
    public class GameAutoMapping : Profile
    {
        public GameAutoMapping()
        {
            // Прямий мапінг
            CreateMap<BaseGameModel, GameEntity>()
                .ForMember(dest => dest.GameKey, opt => opt.MapFrom(src => src.Key));

            CreateMap<GameModel, GameEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Game.Name))
                .ForMember(dest => dest.GameKey, opt => opt.MapFrom(src => src.Game.Key))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Game.Description))
                .ForMember(dest => dest.GameGenres, opt => opt.MapFrom(src =>
                    src.Genres.Select(gid => new GameGenreEntity { GenreId = gid })))
                .ForMember(dest => dest.GamePlatforms, opt => opt.MapFrom(src =>
                    src.Platforms.Select(pid => new GamePlatformEntity { PlatformId = pid })));

            // Зворотній мапінг
            CreateMap<GameEntity, BaseGameModel>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.GameKey));

            CreateMap<GameEntity, GameModel>()
                .ForMember(dest => dest.Game, opt => opt.MapFrom(src => new BaseGameModel
                {
                    Id = src.Id,
                    Name = src.Name,
                    Key = src.GameKey,
                    Description = src.Description
                }))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                    src.GameGenres.Select(gg => gg.GenreId)))
                .ForMember(dest => dest.Platforms, opt => opt.MapFrom(src =>
                    src.GamePlatforms.Select(gp => gp.PlatformId)));

            // 🎯 Мапінг між базовими моделями
            CreateMap<BaseGameDTO, BaseGameModel>()
                    .ReverseMap();

            // 🎯 Мапінг між обгортками
            CreateMap<GameCreateDTO, GameModel>()
                    .ReverseMap();

            CreateMap<BaseGameWithIdDTO, BaseGameModel>()
                   .ReverseMap();

            CreateMap<BaseGameRequiredDTO, BaseGameModel>()
                  .ReverseMap();

            CreateMap<GamePutRequiredDTO, GameModel>()
                   .ReverseMap();


        }

    }
}

