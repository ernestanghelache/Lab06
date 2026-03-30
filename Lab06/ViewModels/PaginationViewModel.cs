using Lab06.Models;

namespace Lab06.ViewModels;

public class PaginationViewModel<T>
{
    public List<T> Items { get; set; } = [];
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (TotalItems + PageSize - 1) / PageSize;
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public List<Category> Categories { get; set; } = [];
    public int? SelectedCategoryId { get; set; }
}
