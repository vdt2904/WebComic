using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class Favourite
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ComicId { get; set; }
        public DateTime? Datelike { get; set; }

        public virtual Comic? Comic { get; set; }
        public virtual User? User { get; set; }
    }
}
