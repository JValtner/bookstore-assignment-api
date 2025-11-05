using BookstoreApplication.DTO.ExternalComics;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services.IService
{
    public interface IVolumesService
    {
        Task<VolumeDTO> GetVolume(int id);
        Task<PaginatedList<VolumeDTO>> GetVolumesByName(string filter, string sortDirection, int page, int pageSize);
    }
}