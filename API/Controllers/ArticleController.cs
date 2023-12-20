using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Dtos.Article;
using Application.Models.Article;
using Domain.Enums;
using Services.Interfaces;

namespace Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ArticleController : BaseController
{
    private readonly IArticleService _articleService;
    public ArticleController(IArticleService articleService) 
        => (_articleService) = (articleService);
    
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult> GetEventById(Guid id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        return Ok(article);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserArticlePreviewModel>>> GetUserEvents([FromQuery] ArticleStatuses status, 
        [FromQuery] PaginationDto? paginationDto)
    {
        var articles = await _articleService.GetUserArticlesAsync(status, paginationDto);
        return Ok(articles);
    }
    [HttpGet]
    [ActionName("popularEvents")]
    public async Task<ActionResult<IEnumerable<ArticlePreviewModel>>> GetPopularArticles([FromQuery] string period, 
        [FromQuery] PaginationDto? paginationDto)
    {
        var articles = await _articleService.GetPopularArticlesAsync(period, paginationDto);
        return Ok(articles);
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateEvent([FromBody]CreateArticleDto createArticleDto)
    {
        var articleModel = await _articleService.CreateArticleAsync(createArticleDto);
        return Ok(articleModel);
    }
    
    /**[HttpPut]
    [Authorize]
    [ActionName("data")]
    public async Task<ActionResult> UpdateArticle(UpdateArticleDataDto updateArticleDataDto)
    {
        var articleModel = await _articleService.UpdateArticleDataAsync(updateArticleDataDto);
        return Ok(articleModel);
    }*/

    /**
    [HttpPut]
    [Authorize]
    [ActionName("analytics")]
    public async Task<ActionResult> UpdateArticleAnalytics(UpdateArticleAnalyticsDto updateArticleAnalyticsDto)
    {
        var result = await _articleService.UpdateArticleAnalyticsAsync(updateArticleAnalyticsDto);
        return Ok(new
        {
            countLikes = result.Item1,
            reting = result.Item2
        });
    }*/
    [HttpDelete("{id:Guid}")]
    [Authorize]
    public async Task<ActionResult> DeleteEvent(Guid id)
    {
        var isSuccess = await _articleService.DeleteArticleAsync(id);
        return Ok(isSuccess);
    }
    /*[HttpPut]
    public async Task<ActionResult> Complain(ArticleComplainDto articleComplainDto)
    {
        return NoContent();
    }*/
}