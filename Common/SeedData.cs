


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

using System.Linq;


namespace Common
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext
                (serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Terms.Any())
                {
                    return;
                }

                context.Terms.AddRange(
                    new Term
                    {
                        Name = "Home",
                        Code = "home",
                        Content = "home page",
                        Sorting = 0,
                        TaxonomyId = 1
                    },

                    new Term
                    {
                        Name = "Sports",
                        Code = "sports",
                        Content = "sports page",
                        Sorting = 100,
                        TaxonomyId = 2
                    },

                    new Term
                    {
                        Name = "Tech",
                        Code = "tech",
                        Content = "tech page",
                        Sorting = 100,
                        TaxonomyId = 3
                    },

                    new Term
                    {
                        Name = "Entertainment",
                        Code = "entertainment",
                        Content = "entertainment page",
                        Sorting = 100,
                        TaxonomyId = 3
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
