﻿using comment_system_01.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace comment_system_01.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int MovieID { get; set; }
        public string UserID { get; set; }
        public string Context { get; set; }
        public int? Parent { get; set; }
        public int Upvote_count { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }


        public virtual Movie Movie { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Upvote> Upvotes { get; set; }
        public virtual ICollection<Image> Images { get; set; }

    }
}