using HttpServerLibrary;
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
    public class AdminEndpoint : BaseEndpoint
    {
        [Get("admin")]
        public IHttpResponseResult GetAdminPanel()
        {
            var file = File.ReadAllText(@"Templates/Pages/AdminPanel/adminPanel.html");
            var token = Context.Request.Cookies["session-token"].ToString().Split('=').GetValue(1);
            string connectionString =
                @"Server=localhost; Database=filmDB; User Id=sa; Password=P@ssw0rd;TrustServerCertificate=true;";
            var connection = new SqlConnection(connectionString);
            var dBcontext = new ORMContext<User>(connection);

            if (!IsAuthorized(Context)) 
            {
                return Redirect("login");
            }
            else 
            {
                Console.WriteLine(token);
                var userID = SessionStorage.GetUserId(token.ToString());
                Console.WriteLine(userID);
                return Html(file);
                
            }
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
