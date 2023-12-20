using Application.Models;

namespace Services.Interfaces;

public interface ISearchService
{
    public Task<SearchModel> Search(string searchText);
}