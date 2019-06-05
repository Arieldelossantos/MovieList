using System;
using System.Threading.Tasks;
using MovieList.Models;

namespace MovieList.Services
{
    public interface IWLApiService
    {
        Task<OMDBResult> SearchMediaListAsync(string title, string page, string type);
        Task<MovieInfo> FindMovieByIMDBId(string imdbid);
    }
}
