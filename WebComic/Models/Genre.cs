using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class Genre
    {
        public Genre()
        {
            ComicGenres = new HashSet<ComicGenre>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ComicGenre> ComicGenres { get; set; }
    }
}
