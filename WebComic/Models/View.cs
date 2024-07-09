using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class View
    {
        public int ViewId { get; set; }
        public int? UserId { get; set; }
        public int? ChapterId { get; set; }
        public DateTime? Dateview { get; set; }

        public virtual Chapter? Chapter { get; set; }
        public virtual User? User { get; set; }
    }
}
