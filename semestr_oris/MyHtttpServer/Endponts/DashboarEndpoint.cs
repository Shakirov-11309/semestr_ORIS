using HttpServerLibrary;
using MyHtttpServer.Core.Templator;
using MyHtttpServer.Models;
using MyHtttpServer.Session;
using MyHttttpServer.Models;
using MyORMLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHtttpServer.Endponts
{
    public class DashboarEndpoint : BaseEndpoint
    {
        [Get("dashboard")]
        public IHttpResponseResult GetPage() 
        {
            //if (!IsAuthorized(Context)) return Redirect("login");
            

            string connectionString =
                @"Server=localhost; Database=filmDB; User Id=sa; Password=P@ssw0rd;TrustServerCertificate=true;";
            var connection = new SqlConnection(connectionString);
            var dBcontext = new ORMContext<Movies>(connection);
            var file = File.ReadAllText(@"Templates/Pages/Dashboard/index.html");
            return Html(file);
        }

        [Get("dashboard/all")]
        public IHttpResponseResult GetPosterUrl() 
        {
            string connectionString =
                @"Server=localhost; Database=filmDB; User Id=sa; Password=P@ssw0rd;TrustServerCertificate=true;";
            var connection = new SqlConnection(connectionString);
            var dBcontext = new ORMContext<Movies>(connection);
            var urls = dBcontext.GetPosterUrl();
            return Json(urls);
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

        public List<string> PutPosterUrlToTemplate(List<string> urls) 
        {
            var result = new List<string>();
            foreach (var url in urls) 
            {
                result.Add(CustomTemplator.GetHtmlByTemplatePosterUrl(url));
            }
            return result;
        }
    }
}
