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
                            Name = "Pokemon Blue",
                            Slug = "pokemon-blue",
                            Description = "The game that started it all.",
                            Family = morgan,
                            Image = "pokemonblue.jpg"
                        },
                        new Member
                        {
                            Name = "Hillary Thornton",
                            Slug = "hillary-thornton",
                            Description = "Daughter of Shoaf and Amanda Thornton",
                            Family = thornton,
                            Image = "hogwartslegacy.jpg"
                        }
                );

                context.SaveChanges();
            }
        }
    }
}