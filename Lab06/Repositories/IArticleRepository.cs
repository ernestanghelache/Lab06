using Lab06.Models;

namespace Lab06.Repositories;

public interface IArticleRepository : IRepository<Article>
{
    Task<List<Article>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<Article?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Article>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<List<Article>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<List<Article>> GetLatestAsync(int count, CancellationToken cancellationToken = default);
    Task<int> CountByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<List<Article>> GetPagedByCategoryAsync(int categoryId, int page, int pageSize, CancellationToken cancellationToken = default);
}