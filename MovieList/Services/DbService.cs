using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieList.Models;

namespace MovieList.Services
{
    public class DbService : IDbService
    {
        public async Task<MovieInfo> MovieInfo(IWLApiService wLApiService, string imdbId)
        {
            var dataResult = await wLApiService.FindMovieByIMDBId(imdbId);

            if (dataResult != null)
            {
                return dataResult;
            }

            return null;
        }

        public async Task<List<Search>> MoviesResult(IWLApiService wLApiService, string movieTitle)
        {
            var dataResult = await wLApiService.SearchMediaListAsync(movieTitle, "1", "movie");

            if (dataResult != null && dataResult.Search.Any())
            {
                return new List<Search>(dataResult.Search);
            }

            return null;
        }
    }
}
