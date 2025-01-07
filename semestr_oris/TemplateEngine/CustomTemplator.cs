﻿using MyHtttpServer.Core.Templator;

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
    }
}
