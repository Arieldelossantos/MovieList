using System;
using System.Net.Http;
using System.Threading.Tasks;
using MovieList.Helpers;
using MovieList.Models;

namespace MovieList.Services
{
    public class WLApiService : IWLApiService
    {
        string OMDBApiKey = AppConstants.OMDBApiKey;
        string OMDBApiEndPoint = AppConstants.OMDBApiEndpoint;

        public async Task<MovieInfo> FindMovieByIMDBId(string imdbid)
        {
            var _endPoint = $"{OMDBApiEndPoint}?i={imdbid}&plot=full&r=json&type=movie&apikey={OMDBApiKey}";
            using (var http = new HttpClient())
            {
                var data = await http.GetStringAsync(_endPoint);

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieInfo>(data);


                return response;
            }
        }

        public async Task<OMDBResult> SearchMediaListAsync(string title, string page, string type)
        {

            var _endPoint = $"{OMDBApiEndPoint}?s={title}&page={page}&type={type}&apikey={OMDBApiKey}";
            using (var http = new HttpClient())
            {
                var data = await http.GetStringAsync(_endPoint);

                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<OMDBResult>(data);

                return response;
            }
        }

    }
}
