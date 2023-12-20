using AutoMapper;
using Application.Dtos.Article;
using Application.Models.Article;
using Domain;

namespace Application.Mapping;

public class ArticleMapper : Profile
{
    public ArticleMapper()
    {
        CreateMap<Article, ArticleModel>().ForMember(model => model.AuthorNickName,
            opt => opt.MapFrom(article => article.Author.NickName))
            .ForMember(model => model.Tags,
                opt => opt.MapFrom(article => article.Tags))
            .ForMember(model => model.CategoryName,
                opt => opt.MapFrom(article => article.Category!.Name));
        
        CreateMap<CreateArticleDto, Article>()
            .ForMember(article => article.CreationDate, 
                opt => opt.MapFrom(dto => DateTime.Now))
            .ForMember(article => article.Tags,
                opt => opt.Ignore());

        CreateMap<UpdateArticleDataDto, Article>();
        CreateMap<Article, ArticlePreviewModel>().ReverseMap();
        CreateMap<Article, UserArticlePreviewModel>();
    }
}