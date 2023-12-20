using Application.Dtos;
using Application.Dtos.ArticleCategory;
using Application.Models.Article;

namespace Services.Interfaces;

public interface IArticleCategoryService
{
    public Task<IEnumerable<ArticleCategoryModel>> GetAllCategoriesAsync();
    public Task<ArticleCategoryModel> GetArticlesInCategory(Guid categoryId, PaginationDto paginationDto);
    public Task<ArticleCategoryModel> CreateArticleCategory(CreateArticleCategoryDto createArticleCategoryDto);
}