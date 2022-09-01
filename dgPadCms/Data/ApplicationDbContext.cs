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

   
        protected override void OnModelCreating(ModelBuilder builder)
        {   
            //// PostTerm: set primary key 
            //builder.Entity<PostTerm>().HasKey(po => new { po.PostId, po.TermId });

            //// PostTerm: set foreign keys 
            //builder.Entity<PostTerm>().HasOne(po => po.Post)
            //    .WithMany(p => p.PostTerms)
            //    .HasForeignKey(pa => pa.PostId);

            //builder.Entity<PostTerm>().HasOne(po => po.Term)
            //   .WithMany(p => p.PostTerms)
            //   .HasForeignKey(pa => pa.TermId);

           base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });

        }


       public DbSet<Term> Terms { get; set; }
    
       public DbSet<PostType> PostType { get; set; }
        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<Post> Posts { get; set; }
    
    }
}
