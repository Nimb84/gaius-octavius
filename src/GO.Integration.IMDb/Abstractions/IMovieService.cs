using GO.Integration.IMDb.Models;

namespace GO.Integration.IMDb.Abstractions
{
    public interface IMovieService
    {
        public Task<List<IMDbMovie>> Search(string query);
    }
}
