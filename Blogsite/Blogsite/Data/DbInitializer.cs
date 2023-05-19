using Blogsite.Models;
using Microsoft.AspNetCore.Identity;

namespace Blogsite.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(AppDbContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    UserName = "user",
                    Email = "user@test.com"
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "User");

                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };
                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] { "User", "Admin" });
            }

            if (context.Tags.Any()) return;

            var tags = new List<Tag>
            {
                new Tag
                {
                    Title = "Art",
                    Slug = "art"
                },
                new Tag
                {
                    Title = "Books",
                    Slug = "books"
                },
                new Tag
                {
                    Title = "Business",
                    Slug = "business"
                },
                new Tag
                {
                    Title = "Design",
                    Slug = "design"
                },
                new Tag
                {
                    Title = "Education",
                    Slug = "education"
                },
                new Tag
                {
                    Title = "Family",
                    Slug = "family"
                },
                new Tag
                {
                    Title = "Fitness",
                    Slug = "fitness"
                },
                new Tag
                {
                    Title = "Food",
                    Slug = "food"
                },
            };

            context.Tags.AddRange(tags);
            context.SaveChanges();
        }
    }
}
