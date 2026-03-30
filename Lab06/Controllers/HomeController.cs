using Lab06.Services;
using Lab06.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IArticleService _articleService;
    private readonly ICategoryService _categoryService;

    public HomeController(IArticleService articleService, ICategoryService categoryService)
    {
        _articleService = articleService;
        _categoryService = categoryService;
    }

    // GET: /Home or /
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var latestArticles = await _articleService.GetLatestAsync(3, cancellationToken);
        var totalArticles = await _articleService.CountAsync(cancellationToken);
        var totalCategories = await _categoryService.CountAsync(cancellationToken);

        var articleViewModels = latestArticles.Select(a => new ArticleViewModel
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            PublishedAt = a.PublishedAt,
            CategoryName = a.Category?.Name ?? "N/A",
            AuthorName = a.User?.Name ?? "N/A",
            ImagePath = a.ImagePath
        }).ToList();

        var viewModel = new HomeViewModel
        {
            LatestArticles = articleViewModels,
            TotalArticles = totalArticles,
            TotalCategories = totalCategories
        };

        return View(viewModel);
    }
}
