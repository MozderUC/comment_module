using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace comment_system_01.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public int? CommentID { get; set; }
        public int ImageSize { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }        
              
        public virtual Comment Comment { get; set; }
        public ImageUrl ImageUrl { get; set; }
    }
}