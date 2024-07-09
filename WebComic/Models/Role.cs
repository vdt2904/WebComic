using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public int? RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
