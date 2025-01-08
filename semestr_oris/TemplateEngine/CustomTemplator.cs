using MyHtttpServer.Core.Templator;

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
                template = template.Replace("{" + property.Name + "}", property.GetValue(obj).ToString());
            }
            return template;
        }

        public static string GetHtmlByTemplatePosterUrl(string url) 
        {
            var template = $@"<div class=""card-item-wrapper"">
                        <picture class=""item-img-template"">
                            <img class=""img-template"" src=""{url}"">
                        </picture>
                        <div class=""item-img-template-shadow""></div>
                        <div class=""item-gradient""></div>
                        <div class=""item-content-img"">
                            <img src=""images/logo-films/logo-2.png"" class=""img-template-logo"">
                            <div class=""item-content-data"">
                                <div class=""content-data-i-1"">
                                    <h6>6.0</h6>
                                </div>
                                <div class=""content-data-other"">
                                    <h6>18+</h6>
                                </div>
                                <div class=""content-data-other"">
                                    <h6>2024</h6>
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
                                    Мальчик встречает дух веселой и отважной девушки 1920-х годов,
                                    после чего между ними возникает необычная, но глубокая связь
                                    сквозь десятилетия.
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
                    </div>";
            return template;
        }
    }
}
