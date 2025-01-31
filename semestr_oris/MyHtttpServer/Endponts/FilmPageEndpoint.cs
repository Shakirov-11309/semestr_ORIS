﻿using HttpServerLibrary;
using Models;
using MyHtttpServer.Core.Templator;
using MyHtttpServer.Session;
using MyORMLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngine.Models;

namespace MyHtttpServer.Endponts
{
    public class FilmPageEndpoint : BaseEndpoint
    {
        [Get("watch")]
        public IHttpResponseResult GetFilmPage(int filmId)
        {
            var file = File.ReadAllText(@"Templates/Pages/FilmPage/page.html");
            string connectionString =
                @"Server=localhost; Database=filmDB; User Id=sa; Password=P@ssw0rd;TrustServerCertificate=true;";
            var dBMovies = new ORMContext<Movies>(new SqlConnection(connectionString));
            var dBMovieActors = new ORMContext<Movies>(new SqlConnection(connectionString));
            var dBMovieDirectors = new ORMContext<Movies>(new SqlConnection(connectionString));
            var dBMovieCountries = new ORMContext<Movies>(new SqlConnection(connectionString));
            var dBMovieGenres = new ORMContext<Movies>(new SqlConnection(connectionString));
            var dataMovie = dBMovies.ReadById(filmId);
            var actors = dBMovieActors.ReadActorsByMovieId(filmId);
            var directors = dBMovieDirectors.ReadDirectorsByMovieId(filmId);
            var countries = dBMovieCountries.ReadCountriesByMovieId(filmId);
            var genres = dBMovieGenres.ReadGenresByMovieId(filmId);
            var result = CustomTemplator.GetHtmlByTemplateFilmPageData(dataMovie, actors, directors, countries, genres, file);
            if (IsAuthorized(Context)) return Html(CustomTemplator.GetHtmlAuthorizatedPage(result));
            return Html(result);
        }

        [Get("watch/all")]
        public IHttpResponseResult GetPosterUrl()
        {
            string connectionString =
                @"Server=localhost; Database=filmDB; User Id=sa; Password=P@ssw0rd;TrustServerCertificate=true;";
            var connection = new SqlConnection(connectionString);
            var dBcontext = new ORMContext<Movies>(connection);
            var data = PutDataToTemplate(dBcontext.GetMovies());
            return Json(data);
        }

        public List<string> PutDataToTemplate(List<Movies> movies)
        {
            var result = new List<string>();
            foreach (var movie in movies)
            {
                result.Add(CustomTemplator.GetHtmlByTemplateSlideBarCardData(movie));
            }
            return result;
        }

        public bool IsAuthorized(HttpRequestContext context)
        {
            // Проверка наличия Cookie с session-token
            if (context.Request.Cookies.Any(c => c.Name == "session-token"))
            {
                var cookie = context.Request.Cookies["session-token"];
                return SessionStorage.ValidateToken(cookie.Value);
            }

            return false;
        }
    }
}
