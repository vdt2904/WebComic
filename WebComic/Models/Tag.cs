using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class Tag
    {
        public Tag()
        {
            ComicTags = new HashSet<ComicTag>();
        }

        public int TagId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ComicTag> ComicTags { get; set; }
    }
}
