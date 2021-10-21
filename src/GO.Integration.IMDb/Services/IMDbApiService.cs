using GO.Integration.IMDb.Abstractions;
using GO.Integration.IMDb.Configurations;
using GO.Integration.IMDb.Models;
using IMDbApiLib;
using Microsoft.Extensions.Options;

namespace GO.Integration.IMDb.Services
{
    internal sealed class IMDbApiService : IMovieService
    {
        private readonly ApiLib _api;

        public IMDbApiService(IOptions<IMDbConfiguration> imdbConfiguration)
        {
            _api = new ApiLib(imdbConfiguration.Value.ApiKey);
        }

        public async Task<List<IMDbMovie>> Search(string query)
        {
            var result = await _api.SearchMovieAsync(query);

            return result.Results?
                .Select(item => new IMDbMovie
                {
                    ExternalId = item.Id,
                    ResultType = item.ResultType,
                    Image = item.Image,
                    Title = item.Title,
                    Description = item.Description
                })
                .ToList() ?? new List<IMDbMovie>();
        }
    }
}
