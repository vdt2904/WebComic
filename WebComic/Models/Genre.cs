using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public virtual ICollection<ComicGenre> ComicGenres { get; set; }
    }
}
