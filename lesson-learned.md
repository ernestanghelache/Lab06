# Demo YouTube
https://youtu.be/-vxccaGzAIg

# Explicarea Structurii proiectului

## Controllers
Primeste requesturile HTTP, delegheaza la Services si returneaza Views, nu are logica de business complexa.

## Services
Contine logica de business

## Repositories
Se ocupa de baza de date

## ViewModels
Controller trimite catre View

## Views
Pagini HTML finale vazute de catre utilizator

# Intrebari

## De ce folosim Repository Pattern?
Il folosim deoarece face abstractie intre controller si baza de date, separand logicile. Daca dorim sa modificam baza de date, nu trebuie sa modificam si controller-ul.

## Ce s-ar întâmpla dacă apelam `_context` direct din controller?
Acesta ar deveni prea incarcat si greu de testat. Refactorizarea ar fi dificila, mai ales daca avem mai multi controlleri unde ar trebui sa schimbam logica in fiecare fisier. De aceea folosim Service Layer.

## De ce avem un Service Layer separat?
Deaorece in acesta pastram logica de business pentru a orchestra interactiunile intre controller si repository.

## Ce logică ar ajunge în controller fără Service Layer?
Controller-ul ar deveni responsabil de citirea datelor sau salvarea bazei de date

## De ce folosim interfețe (IArticleRepository, IArticleService)?
Pentru abstractizare si flexibilitate, acestea definesc un contract, nu o implementare.
```csharp
// IArticleService.cs - Interfata
public interface IArticleService
{
    Task<List<Article>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Article?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Article article, CancellationToken cancellationToken = default);
    Task UpdateAsync(Article article, CancellationToken cancellationToken = default);
}

// ArticleService.cs - Implementare
public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task AddAsync(Article article, CancellationToken cancellationToken = default)
    {
        article.PublishedAt = DateTime.Now;
        await _unitOfWork.ArticleRepository.AddAsync(article, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

// ArticlesController.cs - Utilizare
public class ArticlesController : Controller
{
    private readonly IArticleService _articleService;  // Nu implementeaza, ci foloseste interfata

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }
}
```
## Cum vă ajută această structură dacă adăugați API REST sau aplicație mobilă?
Logica de business ramane aceeasi, deci aplicatia este usor de reutilizat si de mentinut.
