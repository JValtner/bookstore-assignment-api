using BookstoreApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApplication.Utils
{
    public static class SeedData
    {
        public static async Task SeedDataAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Ensure roles exist
            string[] roles = { "Editor", "Librarian" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // --- Editor User ---
            string editorEmail = "editor@bookstore.com";
            string editorUserName = "Editor123";

            var editorUser = await userManager.FindByNameAsync(editorUserName);
            if (editorUser == null)
            {
                editorUser = new ApplicationUser
                {
                    UserName = editorUserName,
                    Email = editorEmail,
                    Name = "John",
                    Surname = "Editor"
                };

                var result = await userManager.CreateAsync(editorUser, "Editor@123!");
                if (!result.Succeeded)
                {
                    Console.WriteLine("Failed to create Editor user:");
                    foreach (var error in result.Errors)
                        Console.WriteLine($" - {error.Description}");
                }
            }

            // Ensure Editor role is applied
            if (!await userManager.IsInRoleAsync(editorUser, "Editor"))
            {
                await userManager.AddToRoleAsync(editorUser, "Editor");
            }

            // --- Librarian User ---
            string librarianEmail = "librarian@bookstore.com";
            string librarianUserName = "Librarian123";

            var librarianUser = await userManager.FindByNameAsync(librarianUserName);
            if (librarianUser == null)
            {
                librarianUser = new ApplicationUser
                {
                    UserName = librarianUserName,
                    Email = librarianEmail,
                    Name = "Ana",
                    Surname = "Librarian"
                };

                var result = await userManager.CreateAsync(librarianUser, "Librarian@123!");
                if (!result.Succeeded)
                {
                    Console.WriteLine("Failed to create Librarian user:");
                    foreach (var error in result.Errors)
                        Console.WriteLine($" - {error.Description}");
                }
            }

            // Ensure Librarian role is applied
            if (!await userManager.IsInRoleAsync(librarianUser, "Librarian"))
            {
                await userManager.AddToRoleAsync(librarianUser, "Librarian");
            }
        }
    }
}
