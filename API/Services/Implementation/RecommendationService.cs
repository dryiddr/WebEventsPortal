using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Application.Dtos.Enums;
using Application.Extensions;
using Application.Models.Article;
using Domain;
using Domain.User;
using Persistence.Infrastructure;
using Services.Interfaces;

namespace Services.Implementation;

public class RecommendationService : IRecommendationService
{
    private readonly IRepository<User> _userRepository;
    private readonly ISearchService _searchService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public RecommendationService(IMapper mapper, IHttpContextAccessor contextAccessor, IRepository<User> userRepository, ISearchService searchService, IArticleService articleService)
    {
        _contextAccessor = contextAccessor;
        _userRepository = userRepository;
        _searchService = searchService;
        _articleService = articleService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ArticlePreviewModel>> GetRecommendation()
    {
        var userId = _contextAccessor.HttpContext!.User.GetCurrentUserId();
        var user = await _userRepository.Query()
            .Include(user => user.Recommendation)
            .FirstAsync(user => user.Id == userId);
        user.Recommendation ??= new Recommendation();
        var searchModel = await _searchService.Search(string.Join(' ', user.Recommendation.FoundWords.ToArray()));
        searchModel.Articles ??= await _articleService.GetPopularArticlesAsync(Periods.week.ToString(), null);
        var recommendationModel = 
            await _mapper.ProjectTo<ArticlePreviewModel>(searchModel.Articles.AsQueryable())
            .ToListAsync();
        return recommendationModel;
    }
}