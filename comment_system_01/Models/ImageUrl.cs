using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace comment_system_01.Models
{
    public class ImageUrl
    {
        [Key]
        [ForeignKey("Image")]
        public int ImageID { get; set; }

        public string imageUrl { get; set; }

        public Image Image{ get; set; }

    }
}