using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class ServicePrice
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public double? ServicePrice1 { get; set; }
        public int? UsedTime { get; set; }

        public virtual Service? Service { get; set; }
    }
}
