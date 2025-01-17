using HttpServerLibrary;
using Models;
using MyHtttpServer.Session;
using MyHttttpServer.Models;
using MyORMLibrary;
using System.Data.SqlClient;
using System.Text.Json;

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
                var userID = SessionStorage.GetUserId(token.ToString());
                var is_admin = dBcontext.CheckUserByValideAdmin(userID);
                if (is_admin)
                {
                    return Html(file);
                }
                else 
                {
                    return Redirect("movies");
                }
            }
        }

        //[Post("admin/handleMovies")]
        //public IHttpResponseResult HandleMovies(HttpRequestContext context)
        //{
        //    try
        //    {
        //        // Чтение тела запроса
        //        var movieData = JsonSerializer.Deserialize<Movies>(context);

        //        // Логика обработки данных
        //        if (context.Request.Headers["X-HTTP-Method-Override"] == "POST")
        //        {
        //            // Логика для добавления фильма
        //            AddMovieToDatabase(movieData);
        //        }
        //        else if (context.Request.Headers["X-HTTP-Method-Override"] == "DELETE")
        //        {
        //            // Логика для удаления фильма
        //            //RemoveMovieFromDatabase(movieData.Title);
        //        }

        //        return Json(new { message = "Успешно обработано" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = "Ошибка обработки запроса", details = ex.Message });
        //    }
        //}

        private void AddMovieToDatabase(Movies movie)
        {
            // Логика добавления фильма в базу данных
            string connectionString = "Server=localhost; Database=filmDB; User Id=sa; Password=P@ssw0rd;TrustServerCertificate=true;";
            using (var connection = new SqlConnection(connectionString))
            {
                var dBcontext = new ORMContext<Movies>(connection);
                dBcontext.Create(movie); 
            }
        }

        //private void RemoveMovieFromDatabase(string title)
        //{
        //    // Логика удаления фильма из базы данных
        //    string connectionString = "Server=localhost; Database=filmDB; User Id=sa; Password=P@ssw0rd;TrustServerCertificate=true;";
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        var dBcontext = new ORMContext<Movies>(connection);
        //        dBcontext.Delete(m => m.Title == title); // Удаление записи по названию
        //    }
        //}

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
