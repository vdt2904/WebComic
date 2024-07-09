using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class ForgetPassword
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime? DateExprired { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
