using HttpServerLibrary;
using MyHtttpServer.Session;
using MyHttttpServer.Models;
using MyORMLibrary;
using System.Data.SqlClient;
using System.Net;

namespace MyHtttpServer.Endponts
{
    public class AuthEndpoint : BaseEndpoint
    {

        [Get("login")]
        public IHttpResponseResult GetLogin()
        {
            var file = File.ReadAllText(@"Templates/Pages/Auth/login.html");
            if (IsAuthorized(Context)) return Redirect("movies");
            return Html(file);
        }

        [Post("login")]
        public IHttpResponseResult AuthPost(string email, string password)
        {
            string connectionString =
                @"Server=localhost; Database=filmDB; User Id=sa; Password=P@ssw0rd;TrustServerCertificate=true;";
            var connection = new SqlConnection(connectionString);
            var dBcontext = new ORMContext<User>(connection);
            var user = dBcontext.ReadByAll($"Email = '{email}' AND Password = '{password}'").FirstOrDefault();
            Console.WriteLine($"Клиент: Email:{email} Пароль:{password}");
            if (user == null)
            {
                var errText = "Сервер: Такого пользователя нет или введены не вырно почта и пароль";
                Console.WriteLine(errText);
                return Json(new { success = false, message = "Неверные почта или пароль." });
            }

            string token = Guid.NewGuid().ToString();
            Cookie cookie = new Cookie("session-token", token);
            Context.Response.SetCookie(cookie);
            Console.Write("Сервер: " + cookie);
            SessionStorage.SaveSession(token, user.id);
            Console.WriteLine($"\nСервер: Успешная авторизация");
            return Json(new { success = true, redirectUrl = "movies" });
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
