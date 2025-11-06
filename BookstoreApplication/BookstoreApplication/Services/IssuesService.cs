using System.Text.Json;
using AutoMapper;
using BookstoreApplication.DTO.ExternalComics;
using BookstoreApplication.Models.ExternalComics;
using BookstoreApplication.Models.IRepository;
using BookstoreApplication.Services.IService;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly IComicsRepository _comicsRepository;
        private readonly IComicVineConnection _comicVineConnection;
        private readonly IConfiguration _config;
        private readonly ILogger<IssuesService> _logger;
        private readonly IMapper _mapper;

        public IssuesService(IComicVineConnection comicVineConnection, IConfiguration configuration, ILogger<IssuesService> logger, IMapper mapper, IComicsRepository comicsRepository)
        {
            _comicsRepository = comicsRepository;
            _comicVineConnection = comicVineConnection;
            _config = configuration;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedList<IssueDTO>> GetIssuesByVolume(string filter, string? sortDirection, int pageIndex, int pageSize)
        {
            int offset = (pageIndex - 1) * pageSize;

            if (string.IsNullOrWhiteSpace(filter))
                return new PaginatedList<IssueDTO>(new List<IssueDTO>(), 0, pageIndex, pageSize);

            // Ensure volumeId has the prefix
            var volumeId = filter.StartsWith("4050-") ? filter : $"4050-{filter}";
            var apiKey = _config["ComicVineAPIKey"];
            var baseUrl = _config["ComicVineBaseUrl"];

            // Extract numeric ID from volumeId
            if (!int.TryParse(volumeId.Split('-').Last(), out int numericVolumeId))
            {
                _logger.LogWarning("Invalid volume ID format: {VolumeId}", volumeId);
                return new PaginatedList<IssueDTO>(new List<IssueDTO>(), 0, pageIndex, pageSize);
            }

            // Step 1: Verify that the volume exists
            var validateUrl = $"{baseUrl}/volume/{volumeId}/?api_key={apiKey}&format=json&field_list=id,name";
            var volumeJson = await _comicVineConnection.Get(validateUrl);

            using (var volumeDoc = JsonDocument.Parse(volumeJson))
            {
                var root = volumeDoc.RootElement;
                if (!root.TryGetProperty("results", out var volResult) || volResult.ValueKind != JsonValueKind.Object)
                {
                    _logger.LogWarning("ComicVine returned no valid volume for {VolumeId}", volumeId);
                    return new PaginatedList<IssueDTO>(new List<IssueDTO>(), 0, pageIndex, pageSize);
                }
            }

            // Step 2: Fetch issues
            var issuesUrl = $"{baseUrl}/issues" +
                            $"?api_key={apiKey}" +
                            $"&format=json" +
                            $"&field_list=id,name,volume,deck,description,issue_number,image,site_detail_url,date_added,date_last_updated" +
                            $"&limit={pageSize}" +
                            $"&offset={offset}" +
                            $"&sort=name:{sortDirection}" +
                            $"&filter=volume:{numericVolumeId}";

            var json = await _comicVineConnection.Get(issuesUrl);
            _logger.LogInformation(json);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            using var doc = JsonDocument.Parse(json);
            var rootElement = doc.RootElement;

            int totalCount = 0;
            List<IssueDTO> items = new();

            if (rootElement.TryGetProperty("number_of_total_results", out var total))
                totalCount = total.GetInt32();

            if (rootElement.TryGetProperty("results", out var results) &&
                results.ValueKind == JsonValueKind.Array)
            {
                items = JsonSerializer.Deserialize<List<IssueDTO>>(results.GetRawText(), options) ?? new();

                // Step 3: Discard wrong-volume issues 
                items = items.Where(i => i.Volume != null && i.Volume.Id == numericVolumeId).ToList();

                if (items.Count == 0)
                    _logger.LogWarning("ComicVine returned mismatched results for volume {VolumeId}", volumeId);
            }

            return new PaginatedList<IssueDTO>(items, totalCount, pageIndex, pageSize);
        }

        public async Task<LocalIssueDTO> AddLocalIssueAsync(LocalIssueDTO dto)
        {
            _logger.LogInformation("Saving local issue {@Dto}", dto);

            var entity = _mapper.Map<LocalIssue>(dto);
            await _comicsRepository.AddAsync(entity);

            _logger.LogInformation("Local issue saved with ID {Id}", entity.Id);
            return _mapper.Map<LocalIssueDTO>(entity);

        }

    }

}
