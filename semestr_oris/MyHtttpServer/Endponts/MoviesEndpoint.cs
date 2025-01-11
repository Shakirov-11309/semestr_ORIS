using HttpServerLibrary;
using Models;
using MyHtttpServer.Core.Templator;
using MyHtttpServer.Session;
using MyORMLibrary;
using System.Data.SqlClient;
using TemplateEngine.Models;

namespace MyHtttpServer.Endponts
{
    public class MoviesEndpoint : BaseEndpoint
    {
        [Get("movies")]
        public IHttpResponseResult GetPage() 
        {
            string connectionString =
                @"Server=localhost; Database=filmDB; User Id=sa; Password=P@ssw0rd;TrustServerCertificate=true;";
            var connection = new SqlConnection(connectionString);
            var dBcontext = new ORMContext<Movies>(connection);
            var file = File.ReadAllText(@"Templates/Pages/Movies/index.html");
            if (IsAuthorized(Context)) return Html(CustomTemplator.GetHtmlAuthorizatedPage(file));
            return Html(file);
        }

        [Get("movies/all")]
        public IHttpResponseResult GetPosterUrl() 
        {
            string connectionString =
                @"Server=localhost; Database=filmDB; User Id=sa; Password=P@ssw0rd;TrustServerCertificate=true;";
            var dBcontext = new ORMContext<Movies>(new SqlConnection(connectionString));
            var movies = dBcontext.GetMovies();
            var data = PutDataToTemplate(movies);
            return Json(data);
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

        public List<string> PutDataToTemplate(List<Movies> movies)
        {
            var result = new List<string>();
            foreach (var movie in movies)
            {
                result.Add(CustomTemplator.GetHtmlByTemplateCardData(movie));
            }
            return result;
        }
    }
}
