using Application.Dtos;
using Domain;

namespace Services.Interfaces;

public interface ITagService
{
    public Task AssignTagsToArticle(IEnumerable<TagDto>? tagDtos, Article article);
}