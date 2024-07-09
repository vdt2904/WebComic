using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class ComicTag
    {
        public int ComicId { get; set; }
        public int TagId { get; set; }
        public string Id { get; set; } = null!;

        public virtual Comic Comic { get; set; } = null!;
        public virtual Tag Tag { get; set; } = null!;
    }
}
