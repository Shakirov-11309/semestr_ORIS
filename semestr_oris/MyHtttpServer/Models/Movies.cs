using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHtttpServer.Models
{
    public class Movies
    {
        public int Id { get; set; }

        public string Tittle { get; set; }

        public string DescriptionCard { get; set; }

        public string Description { get; set; }
        public int ReleaseYear { get; set; }

        public int Rating { get; set; }

        public float ImdbRating { get; set; }

        public float AmediatiatekaRating { get; set; }

        public string PosterUrl { get; set; }

        public string BgUrl { get; set; }

        public string CardUrl { get; set;}
    }
}
