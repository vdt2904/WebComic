using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class ComicGenre
    {
        public int ComicId { get; set; }
        public int GenreId { get; set; }
        public int Id { get; set; }

        public virtual Comic Comic { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
    }
}
