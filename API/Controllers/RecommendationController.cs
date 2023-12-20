using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Application.Models.Article;
using Services.Interfaces;

namespace Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RecommendationController : BaseController
{
    private readonly IRecommendationService _recommendationService;

    public RecommendationController(IRecommendationService recommendationService) =>
        (_recommendationService) = (recommendationService);

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ArticlePreviewModel>>> GetRecommendation()
    {
        var recommendation = await _recommendationService.GetRecommendation();
        return Ok(recommendation);
    }
}