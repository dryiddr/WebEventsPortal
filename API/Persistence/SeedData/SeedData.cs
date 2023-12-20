using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Enums;
using Domain.User;

namespace Persistence.SeedData;

public static class SeedData
{
    public static void Seed(this ModelBuilder builder)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@gmail.com",
            Password = "12345",
            Role = "user",
            NickName = "@testU5er",
            Name = "TesT",
            Description = "I`m TesT!",
            RegistrationDate = DateTime.Now
        };
        var article = new Article
        {
            Id = Guid.NewGuid(),
            AuthorId = user.Id,
            Name = "First event",
            KeyWords = new List<string> {"example", "event"},
            Text = "This is example event",
            CreationDate = DateTime.Now,
            Status = ArticleStatuses.Published,
        };
        var tag = new Tag
        {
            Id = Guid.NewGuid(),
            Name = "#sport",
        };
        var category = new ArticleCategory
        {
            Id = Guid.NewGuid(),
            Name = "Sport category",
        };
        var commentary = new Commentary
        {
            Id = Guid.NewGuid(),
            ArticleId = article.Id,
            AuthorId = user.Id,
            Text = "Good event!",
            CreationDate = DateTime.Now,
            CountLikes = 11,
            CountDislikes = 1
        };
        article.CategoryId = category.Id;

        builder.Entity<User>().HasData(user);
        builder.Entity<Article>().HasData(article);
        builder.Entity<ArticleCategory>().HasData(category);
        builder.Entity<Commentary>().HasData(commentary);
        builder.Entity<Tag>().HasData(tag);
    }
}