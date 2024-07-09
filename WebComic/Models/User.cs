using System;
using System.Collections.Generic;

namespace WebComic.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            CommentsChapters = new HashSet<CommentsChapter>();
            Favourites = new HashSet<Favourite>();
            ForgetPasswords = new HashSet<ForgetPassword>();
            RefreshTokens = new HashSet<RefreshToken>();
            UseServices = new HashSet<UseService>();
            Views = new HashSet<View>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentsChapter> CommentsChapters { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<ForgetPassword> ForgetPasswords { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<UseService> UseServices { get; set; }
        public virtual ICollection<View> Views { get; set; }
    }
}
