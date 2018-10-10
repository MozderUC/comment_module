using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace comment_system_01.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Upvote> Upvotes { get; set; }

        public ApplicationUser()
        {
        }
    }
}