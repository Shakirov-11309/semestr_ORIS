using HttpServerLibrary;
using MyHtttpServer.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHtttpServer.Endponts
{
    public class FilmPageEndpoint : BaseEndpoint
    {
        [Get("filmpage")]
        public IHttpResponseResult GetLogin()
        {
            var file = File.ReadAllText(@"Templates/Pages/FilmPage/page.html");
            //if (IsAuthorized(Context)) return Redirect("dashboard");
            return Html(file);
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
