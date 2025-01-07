﻿using HttpServerLibrary;
using MyHtttpServer.Session;
using System;
using System.Collections.Generic;
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
            if (!IsAuthorized(Context)) return Redirect("login");
            var file = File.ReadAllText(@"Templates/Pages/Dashboard/index.html");
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
