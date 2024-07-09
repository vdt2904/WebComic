using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public int? ComicId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CommentDate { get; set; }

        public virtual Comic? Comic { get; set; }
        public virtual User? User { get; set; }
    }
}
