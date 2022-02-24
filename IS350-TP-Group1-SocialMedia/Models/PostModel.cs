using System;
using System.ComponentModel.DataAnnotations;

namespace IS350_TP_Group1_SocialMedia.Models
{
    public class Post
    {
        [Key]
        public int postID { get; set; }
        public string userName { get; set; }
        public string content { get; set; }
        public DateTime sendDate { get; set; }
        public string picturePath { get; set; }
        public int thumbUpNum { get; set; }
    }
}
