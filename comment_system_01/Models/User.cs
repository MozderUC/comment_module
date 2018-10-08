using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace comment_system_01.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Upvote> Upvotes { get; set; }

    }
}