namespace CourseProject.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                // If the database is empty, populate sample data in it

                CreateUser(context, "admin@gmail.com", "123", "System Administrator");
                CreateUser(context, "suerteto@yahoo.co.uk", "123", "suerteto");
                CreateUser(context, "fr0st@yandex.ru", "123", "fr0st");

                CreateRole(context, "Administrators");
                AddUserToRole(context, "admin@gmail.com", "Administrators");
                AddUserToRole(context, "suerteto@yahoo.co.uk", "Administrators");
                AddUserToRole(context, "fr0st@yandex.ru", "Administrators");

                CreatePost(context,
                    title: "Read before posting",
                    body: @"All regular rules for better experience for everyone",
                    date: new DateTime(2016, 08, 31, 17, 53, 48),
                    authorUsername: "suerteto@yahoo.co.uk"
                );

                CreatePost(context,
                    title: "Mod ideas, suggestions and requests",
                    body: @"On this topic you can share with us all your mod related ideas, suggestions and requests",
                    date: new DateTime(2016, 08, 31, 17, 53, 48),
                    authorUsername: "suerteto@yahoo.co.uk"
                );

                CreatePost(context,
                    title: "General SW discussion",
                    body: @"Here you can talk about everything Star Wars related",
                    date: new DateTime(2016, 08, 31, 17, 53, 48),
                    authorUsername: "suerteto@yahoo.co.uk"
                );



                CreateArticle(context,
                    title: "Frequently asked questions",
                    body: @"It's a very useful topic, here in the comments you can ask mod related questions after you first check the previous comments and the main Q&A on the ModDB mod page.",
                    date: new DateTime(2016, 08, 31, 17, 36, 52),
                    authorUsername: "suerteto@yahoo.co.uk"
                );

                context.SaveChanges();
            }
        }

        private void CreateUser(ApplicationDbContext context,
            string email, string password, string fullName)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private void AddUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private void CreatePost(ApplicationDbContext context,
            string title, string body, DateTime date, string authorUsername)
        {
            var post = new Post();
            post.Title = title;
            post.Body = body;
            post.Date = date;
            post.Author = context.Users.Where(u => u.UserName == authorUsername).FirstOrDefault();
            context.Posts.Add(post);
        }

        private void CreateArticle(ApplicationDbContext context,
           string title, string body, DateTime date, string authorUsername)
        {
            var article = new Article();
            article.Title = title;
            article.Body = body;
            article.Date = date;
            article.Author = context.Users.Where(u => u.UserName == authorUsername).FirstOrDefault();
            context.Articles.Add(article);
        }
    }
}
