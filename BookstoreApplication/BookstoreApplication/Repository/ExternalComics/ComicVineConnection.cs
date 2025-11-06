using System.Net;
using System.Text.Json;
using BookstoreApplication.Models.IRepository;
using BookstoreApplication.Exceptions;

namespace BookstoreApplication.Repository.NewFolder
{
    public class ComicVineConnection : IComicVineConnection
    {
        private readonly HttpClient _client;
        private readonly ILogger<ComicVineConnection> _logger;

        public ComicVineConnection(HttpClient client, ILogger<ComicVineConnection> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<string> Get(string url)
        {
            // Zahtev je definisan spram ComicVine dokumentacije
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.ParseAdd("BookstoreApp");

            HttpResponseMessage response = await _client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            // Konverzija JSON sadržaja u JSON dokument da bi se
            // proverila uspešnost zahteva prema API dokumentaciji
            JsonDocument jsonDocument = JsonDocument.Parse(json);

            // Provera da li je odgovor uspešan (200)
            if (!response.IsSuccessStatusCode)
                HandleUnsuccessfulRequest(response, jsonDocument);

            int statusCode = jsonDocument.RootElement.GetProperty("status_code").GetInt32();
            // Prema API dokumentaciji, 1 znači da je uspešan zahtev
            if (statusCode != 1)
                HandleUnsuccessfulRequest(response, jsonDocument);

            return json;
        }


        private void HandleUnsuccessfulRequest(HttpResponseMessage response, JsonDocument jsonDocument)
        {
            var errorMessage = "";
            try
            {
                // Prema API dokumentaciji, ako API vrati odgovor sa greškom,
                // opis greške se nalazi u telu odgovora pod "error" ključem
                errorMessage = jsonDocument.RootElement.GetProperty("error").GetString();
                _logger.LogError($"Request to API failed: {(int)response.StatusCode} - {response.ReasonPhrase}: {errorMessage}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured with message: {ex.Message}");
            }

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new RateLimitException();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedApiAccessException();
            }
            else
            {
                string apiError = string.IsNullOrEmpty(errorMessage) ?
                  "Error occured when sending request to the external API" : errorMessage;
                throw new ApiCommunicationException(apiError);
            }
        }

    }

}