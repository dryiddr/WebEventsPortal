using Application.Dtos;
using Domain;

namespace Services.Interfaces;

public interface IPaginationService
{
    IEnumerable<Article>
        GetArticlesByPagination(IEnumerable<Article> articles, PaginationDto? paginationDto);
}