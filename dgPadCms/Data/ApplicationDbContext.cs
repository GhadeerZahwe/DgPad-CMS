using dgPadCms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace dgPadCms.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Changing table names
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });

        }


       public DbSet<Term> Terms { get; set; }
    
       public DbSet<PostType> PostType { get; set; }
        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<Post> Posts { get; set; }
        //public DbSet<PostTerm> PostTerms { get; set; }
    }
}
