using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class UseService
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ServiceId { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateExprired { get; set; }
        public int? Status { get; set; }

        public virtual Service? Service { get; set; }
        public virtual User? User { get; set; }
    }
}
