using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using WebJetTask.Models;

namespace WebJetTask.Controllers.Api
{
    public class MovieDbController : ApiController
    {
        [HttpGet, Route("api/MovieDb/SearchMovies/{Title?}")]
        public async Task<IEnumerable> SearchMovies(String Title = null)
        {
            List<MovieBank> MovieCollection = new List<MovieBank>();




            //retrieve cinema world movies
            MovieBank CinemaWorldBank = await GetMovies("cinemaworld");
            if (CinemaWorldBank != null && CinemaWorldBank.Movies.Count() > 0)
            {
                MovieCollection.Add(CinemaWorldBank);
            }

            //retrieve cinema world movies
            MovieBank FilmWorldBank = await GetMovies("filmworld");
            if (FilmWorldBank != null && FilmWorldBank.Movies.Count() > 0)
            {
                MovieCollection.Add(FilmWorldBank);
            }
            var MovieList = MovieCollection.SelectMany(x => x.Movies.Select(y => new { y.Title, y.ID, y.Poster, y.Year, x.MovieProvider }));

            if (!String.IsNullOrEmpty(Title))
            {
                MovieList = MovieList.Where(x => x.Title.IndexOf(Title, StringComparison.CurrentCultureIgnoreCase) > -1);
            }
            //var GroupedMovies = from EachMovie in MovieList
            //                    group by new { EachMovie.Title, EachMovie.Poster, EachMovie.Year, EachMovie.MovieProvider } into grouped;

            //var GroupedMovies=MovieList.GroupBy(x => new { x.Title, x.Poster, x.Year }, (key, group) => new { key.Title, key.Poster, key.Year, Provider = group.ToList() });
            var GroupedMovies = MovieList.GroupBy(x => new { x.Title, x.Year }, (key, group) => new { key.Title, key.Year, Provider = group.ToList() });

            return GroupedMovies;
        }
        [HttpGet, Route("api/MovieDb/getmoviebyid/{Provider}/{MovieID}")]
        public async Task<MovieDetails> GetMovieByID(String Provider, String MovieID)
        {
            MovieDetails FoundMovie =await GetMovie(Provider, MovieID);
            return FoundMovie;
        }
        private static async Task<MovieBank> GetMovies(String Provider)
        {
            try
            {
                HttpClient Fetcher = new HttpClient();
                Fetcher.BaseAddress = new Uri("http://webjetapitest.azurewebsites.net/");
                Fetcher.DefaultRequestHeaders.Add("x-access-token", "sjd1HfkjU83ksdsm3802k");
                HttpResponseMessage response = await Fetcher.GetAsync("api/" + Provider + "/movies");
                if (response.IsSuccessStatusCode)
                {
                    MovieBank Bank = await response.Content.ReadAsAsync<MovieBank>();
                    Bank.MovieProvider = Provider;
                    return Bank;
                }
                else
                {
                    //Something has gone wrong, handle it here
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }


        private static async Task<MovieDetails> GetMovie(String Provider, String MovieID)
        {
            try
            {
                HttpClient Fetcher = new HttpClient();
                Fetcher.BaseAddress = new Uri("http://webjetapitest.azurewebsites.net/");
                Fetcher.DefaultRequestHeaders.Add("x-access-token", "sjd1HfkjU83ksdsm3802k");
                HttpResponseMessage response = await Fetcher.GetAsync("api/" + Provider + "/movie/" + MovieID);
                if (response.IsSuccessStatusCode)
                {
                    MovieDetails Bank = await response.Content.ReadAsAsync<MovieDetails>();
                    Bank.MovieProvider = Provider;
                    return Bank;
                }
                else
                {
                    //Something has gone wrong, handle it here
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }



    }
}
