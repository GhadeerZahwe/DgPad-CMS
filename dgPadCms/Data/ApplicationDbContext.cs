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
           

           base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            builder.Entity<PostTerm>()
            .HasKey(bc => new { bc.TermId, bc.PostId });
            builder.Entity<PostTerm>()
                .HasOne(bc => bc.Term)
                .WithMany(b => b.PostTerms)
                .HasForeignKey(bc => bc.TermId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<PostTerm>()
                .HasOne(bc => bc.Post)
                .WithMany(c => c.PostTerms)
                .HasForeignKey(bc => bc.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        }


       public DbSet<Term> Terms { get; set; }
        public DbSet<TaxonomyPostType> TaxonomyPostTypes { get; set; }
        public DbSet<PostType> PostType { get; set; }
        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<Post> Posts { get; set; }
    
    }
}
