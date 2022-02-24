using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using IS350_TP_Group1_SocialMedia.Models;

namespace IS350_TP_Group1_SocialMedia.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<IS350_TP_Group1_SocialMedia.Models.Post> Post { get; set; }
    }
}
