using AutoMapper;
using Application.Dtos.ArticleCategory;
using Application.Models.Article;
using Domain;

namespace Application.Mapping;

public class ArticleCategoryMapper : Profile
{
    public ArticleCategoryMapper()
    {
        CreateMap<ArticleCategory, ArticleCategoryModel>()
            .ForMember(model => model.CountArticles, 
                opt => opt.MapFrom(category => category.Articles!.Count));

        
        CreateMap<CreateArticleCategoryDto, ArticleCategory>().ReverseMap();
    }
}