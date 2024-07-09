using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class Service
    {
        public Service()
        {
            ServicePrices = new HashSet<ServicePrice>();
            UseServices = new HashSet<UseService>();
        }

        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }

        public virtual ICollection<ServicePrice> ServicePrices { get; set; }
        public virtual ICollection<UseService> UseServices { get; set; }
    }
}
