using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateEngine.Models
{
    public class FilmWithGernres
    {
        public Movies Movie { get; set; }

        public TempleteData Genre { get; set; }
    }
}
