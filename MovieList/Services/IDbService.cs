using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieList.Models;

namespace MovieList.Services
{
    public interface IDbService
    {
        Task<List<Search>> MoviesResult(IWLApiService wLApiService, string movieTitle);
        Task<MovieInfo> MovieInfo(IWLApiService wLApiService, string imdbId);
    }
}
