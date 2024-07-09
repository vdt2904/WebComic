using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class CommentsChapter
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public int? ChapterId { get; set; }
        public string? Content { get; set; }
        public DateTime? CommentDate { get; set; }

        public virtual Chapter? Chapter { get; set; }
        public virtual User? User { get; set; }
    }
}
