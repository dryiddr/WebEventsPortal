using AutoMapper;
using Application.Dtos.Commentary;
using Application.Models;
using Application.Models.Commentary;
using Domain;

namespace Application.Mapping;

public class CommentaryMapper : Profile
{
    public CommentaryMapper()
    {
        CreateMap<Commentary, CommentaryModel>(); /*
            .ForMember(model => model.AuthorNickName,
                opt => opt.MapFrom(commentary => commentary.Author.AuthorNickName))
            .ForMember(model => model.Parent,
                opt => opt.MapFrom(commentary => commentary.Parent));*/
        CreateMap<AddCommentaryDto, Commentary>()
            .ForMember(commentary => commentary.CreationDate,
                opt => opt.MapFrom(dto => DateTime.Now))
            .ForMember(commentary => commentary.ArticleId,
                opt => opt.MapFrom(dto => dto.ArticleId))
            .ForMember(commentary => commentary.Text,
                opt => opt.MapFrom(dto => dto.Text));
    }
}