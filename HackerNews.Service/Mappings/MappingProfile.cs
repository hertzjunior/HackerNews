using AutoMapper;
using HackerNews.Service.Dto;
using System;
using Model = HackerNews.Domain.Model;

namespace HackerNews.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Model.Story, Story>()
                .ForMember(dst => dst.Uri, opt => opt.MapFrom(src => src.url))
                .ForMember(dst => dst.PostedBy, opt => opt.MapFrom(src => src.by))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(x => DateTimeOffset.FromUnixTimeSeconds(x.time).DateTime))
                .ForMember(dst => dst.CommentCount, opt => opt.MapFrom(src => src.descendants))
                .ReverseMap();
        }
    }
}
