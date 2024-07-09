using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class ChapterImage
    {
        public int ImageId { get; set; }
        public int? ChapterId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int ImageOrder { get; set; }

        public virtual Chapter? Chapter { get; set; }
    }
}
