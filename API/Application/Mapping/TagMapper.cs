using AutoMapper;
using Application.Models;
using Domain;

namespace Application.Mapping;

public class TagMapper : Profile
{
    public TagMapper()
    {
        CreateMap<Tag, TagModel>().ReverseMap();
    }
}