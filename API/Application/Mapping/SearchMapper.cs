using AutoMapper;
using Application.Models;
using Domain;

namespace Application.Mapping;

public class SearchMapper : Profile
{
    public SearchMapper()
    {
        CreateMap<IEnumerable<Article>, SearchModel>()
            .ForMember(model => model.Articles,
                opt => opt.MapFrom(articles => articles));
    }
}