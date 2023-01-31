using Memories.Entities;
using Microsoft.EntityFrameworkCore;
using Memories.Models;

namespace Memories.Data
{
    public class SeedData
    {
        public static void SeedDatabase(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (!context.Members.Any())
            {

                Family morgan = new Family { Name = "Morgan", Slug = "morgan" };
                Family thornton = new Family { Name = "Thornton", Slug = "thornton" };

                context.Members.AddRange(
                        new Member
                        {
                            Name = "Tyler Morgan",
                            Slug = "tyler-morgan",
                            Description = "Son of Toby and Kim Morgan",
                            Family = morgan,
                            Image = "Headshot.jpeg"
                        },
                        new Member
                        {
                            Name = "Hillary Thornton",
                            Slug = "hillary-thornton",
                            Description = "Daughter of Shoaf and Amanda Thornton",
                            Family = thornton,
                            Image = "HillaryEpcot.jpg"
                        }
                );

                context.SaveChanges();
            }
        }
    }
}