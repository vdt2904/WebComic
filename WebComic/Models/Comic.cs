using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class Comic
    {
        public Comic()
        {
            Chapters = new HashSet<Chapter>();
            ComicGenres = new HashSet<ComicGenre>();
            ComicTags = new HashSet<ComicTag>();
            Comments = new HashSet<Comment>();
            Favourites = new HashSet<Favourite>();
        }

        public int ComicId { get; set; }
        public string Title { get; set; } = null!;
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? CoverImage { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<ComicGenre> ComicGenres { get; set; }
        public virtual ICollection<ComicTag> ComicTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
    }
}
