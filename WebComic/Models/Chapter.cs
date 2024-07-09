using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class Chapter
    {
        public Chapter()
        {
            ChapterImages = new HashSet<ChapterImage>();
            CommentsChapters = new HashSet<CommentsChapter>();
            Views = new HashSet<View>();
        }

        public int ChapterId { get; set; }
        public int? ComicId { get; set; }
        public int ChapterNumber { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }

        public virtual Comic? Comic { get; set; }
        public virtual ICollection<ChapterImage> ChapterImages { get; set; }
        public virtual ICollection<CommentsChapter> CommentsChapters { get; set; }
        public virtual ICollection<View> Views { get; set; }
    }
}
