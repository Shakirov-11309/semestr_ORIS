using Models;
using MyHtttpServer.Core.Templator;
using System.Text.RegularExpressions;
using TemplateEngine.Models;

namespace MyHtttpServer.Core.Templator
{
    public class CustomTemplator : ICustomTemplator
    {
        public string GetHtmlByTemplate(string template, string name)
        {
            return template.Replace("{name}", name);
        }

        public string GetHtmlByTemplate<T>(string template, T obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                var placeholder = $"{{{{{property.Name}}}}}";
                template = template.Replace(placeholder, property.GetValue(obj)?.ToString(), StringComparison.OrdinalIgnoreCase);
            }
            return template;
        }

        public static string GetHtmlAuthorizatedPage(string page)
        {
            // Регулярное выражение для поиска блока
            string pattern = """
<button class="btn-1">Ввести промокод</button> <button class="btn-2" style="margin-right: 0"><a href="login">Войти</a></button> <button class="btn-3">Попробовать за 1₽</button>
""";

            // Замена блока
            string replacement = @"
<div class=""navbar-items"">
    <img src=""images/icon-profile.jpg"" width=""50"" height=""50"" class=""icon-profile-wrapper"">
</div>";

            // Заменяем блок с учетом отступов
            page = page.Replace(pattern, replacement);
            return page;
        }

        public static string GetHtmlByTemplateSlideBarCardData(Movies movies) 
        {
            var template = $@"<a href=""watch?filmId={movies.Id}"" style=""margin-bottom: 12px"">
    <div class=""Movie-CardItem-ImageWrapper"">
        <div class=""Movie-CardItem-PG"">
            <span>{movies.rating}+</span>
        </div>
        <img src=""{movies.bg_url}"" class=""SlideBar-Img"">
    </div>
</a>
<a>
    <span class=""SlideBar-Tittle"">{movies.title}</span>
</a>";
            return template;
        }

        public static string GetHtmlByTemplateCardData(Movies movies)
        {
            var template = $@"<a href=""watch?filmId={movies.Id}"">
        <div class=""card-item-wrapper"">
            <picture class=""item-img-template"">
                <img class=""img-template"" src=""{movies.poster_url}"">
            </picture>
            <div class=""item-img-template-shadow""></div>
            <div class=""item-gradient""></div>
            <div class=""item-content-img"">
                <img src=""{movies.card_url}"" class=""img-template-logo"">
                <div class=""item-content-data"">
                    <div class=""content-data-i-1"">
                        <h6>{movies.amediateka_rating}</h6>
                    </div>
                    <div class=""content-data-other"">
                        <h6>{movies.rating}+</h6>
                    </div>
                    <div class=""content-data-other"">
                        <h6>{movies.release_year}</h6>
                    </div>
                    <div class=""content-data-other"">
                        <h6>ФЭНТЕЗИ</h6>
                    </div>
                    <div class=""content-data-other"">
                        <h6>КОМЕДИЯ</h6>
                    </div>
                    <div class=""content-data-other"">
                        <h6>МЕЛОДРАМА</h6>
                    </div>
                </div>
                <div class=""item-content-data-context"">
                    <h5>
                        {movies.description_card}
                    </h5>
                </div>
                <div class=""item-content-data-buttons"">
                    <button class=""button-in-card-watch"">
                        <img src=""images/play-button.svg"" style=""height: 16px; width: 16px; margin-right: calc(10px/1.08)"">
                        Смотреть
                    </button>
                    <button type=""button"" class=""button-in-card-add"">
                        <img src=""images/add-button.svg"" style=""height: 16px; width: 16px; margin-right: calc(10px/1.08)"">
                        В избранное
                    </button>
                </div>
            </div>
        </div>
    </a>";
            return template;
        }

        public static string GetHtmlByTemplateFilmPageData(Movies movies, string template) 
        {
            template = template.Replace("{{posterUrl}}", movies.bg_url);
            template = template.Replace("{{Logo}}", movies.card_url); 
            template = template.Replace("{{Title}}", movies.title);
            template = template.Replace("{{amediatekaRating}}", movies.amediateka_rating.ToString());
            template = template.Replace("{{imdbRating}}", movies.Imdb_rating.ToString());
            template = template.Replace("{{Year}}", movies.release_year.ToString());
            template = template.Replace("{{Rating}}", movies.rating.ToString());
            template = template.Replace("{{Description}}", movies.description);
            return template;
        }
    }
}
