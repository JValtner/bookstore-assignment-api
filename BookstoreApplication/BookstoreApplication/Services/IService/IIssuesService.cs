using BookstoreApplication.DTO.ExternalComics;
using BookstoreApplication.Utils;
namespace BookstoreApplication.Services.IService
{
    public interface IIssuesService
    {
        Task<PaginatedList<IssueDTO>> GetIssuesByVolume(string filter, string? sortDirection, int pageIndex, int pageSize);
        Task<LocalIssueDTO> AddLocalIssueAsync(LocalIssueDTO localIssue);
    }
}