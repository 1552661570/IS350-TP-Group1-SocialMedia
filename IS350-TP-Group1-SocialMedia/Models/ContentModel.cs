using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace IS350_TP_Group1_SocialMedia.Models
{
    public class ContentModel
    {
        [Required]
        public string content { get; set; }
        public IFormFile picturePath { get; set; }
    }
}
