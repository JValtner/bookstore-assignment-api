using System.Text.Json;
using BookstoreApplication.Models.ExternalComics;
using BookstoreApplication.Models.IRepository;
using BookstoreApplication.Services.IService;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services
{
    public class VolumesService : IVolumesService
    {
        private readonly IComicVineConnection _comicVineConnection;
        private readonly IConfiguration _config;
        private readonly ILogger<VolumesService> _logger;

        public VolumesService(IComicVineConnection comicVineConnection, IConfiguration configuration, ILogger<VolumesService> logger)
        {
            _comicVineConnection = comicVineConnection;
            _config = configuration;
            _logger = logger;
        }

        public async Task<PaginatedList<VolumeDTO>> GetVolumesByName(string filter, string sortDirection, int pageIndex, int pageSize)
        {
            int offset = (pageIndex - 1) * pageSize;

            var url = $"{_config["ComicVineBaseUrl"]}/volumes" +
                      $"?api_key={_config["ComicVineAPIKey"]}" +
                      $"&format=json" +
                      $"&field_list=id,name,deck,image,site_detail_url,date_added,date_last_updated" +
                      $"&limit={pageSize}" +
                      $"&offset={offset}"+
                      $"&sort=name:{sortDirection}";


            if (!string.IsNullOrWhiteSpace(filter))
            {
                url += $"&filter=name:{Uri.EscapeDataString(filter)}";
            }


            var json = await _comicVineConnection.Get(url);
            _logger.LogInformation(json);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            int totalCount = 0;
            List<VolumeDTO> items = new();

            if (root.TryGetProperty("number_of_total_results", out var total))
            {
                totalCount = total.GetInt32();
            }

            if (root.TryGetProperty("results", out var results) &&
                results.ValueKind == JsonValueKind.Array)
            {
                items = JsonSerializer.Deserialize<List<VolumeDTO>>(results.GetRawText(), options);
            }

            return new PaginatedList<VolumeDTO>(items, totalCount, pageIndex, pageSize);
        }
        public async Task<VolumeDTO> GetVolume(int id)
        {
            var url = $"{_config["ComicVineBaseUrl"]}/volume/{id}" +   
                      $"?api_key={_config["ComicVineAPIKey"]}" +
                      $"&format=json" +
                      $"&field_list=id,name,deck,image,site_detail_url,date_added,date_last_updated";

            var json = await _comicVineConnection.Get(url);

            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("results", out var result) &&
                result.ValueKind == JsonValueKind.Object)
            {
                return new VolumeDTO
                {
                    Id = result.GetProperty("id").GetInt32(),
                    Name = result.GetProperty("name").GetString(),
                    Deck = result.GetProperty("deck").GetString(),
                    Image = new ComicVineImage
                    {
                        Icon_url = result.GetProperty("image").GetProperty("icon_url").GetString(),
                        Medium_url = result.GetProperty("image").GetProperty("medium_url").GetString(),
                        Super_url = result.GetProperty("image").GetProperty("super_url").GetString()
                    },
                    Site_detail_url = result.GetProperty("site_detail_url").GetString(),
                    Date_added = result.GetProperty("date_added").GetString(),
                    Date_last_updated = result.GetProperty("date_last_updated").GetString()
                };
            }

            return null;
        }

    }
}
