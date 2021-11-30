using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;

public class DbInitializer
{
    public static Task SeedingAsync(ApplicationDbContext context)
    {

        context.Database.EnsureCreated();
        // context.Database.EnsureDeleted();
        // context.Users.RemoveRange();
        context.SaveChanges();


        //データが存在しない場合
        if (!context.Users.Any())
        {
            var user = new Account
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "システム管理者",
                NormalizedUserName = "システム管理者",
                Email = "admin",
                NormalizedEmail = "ADMIN",
                Password = "Admin#5970##",
                PasswordHash = "AQAAAAEAACcQAAAAEG7M4zysyVi5bcxcUma4OxwMj5P5SsDrIZlpL4OWnn8GWGi8GoRNro7ot/oRJx3P9g==",
                EmailConfirmed = true,
                ActiveFlg = true,
                DisplayOrder = 1,
                Color = "blue",
                FirstChar = "a",
            };
            context.Users.Add(user);

            // 変更をデータベースに反映
            context.SaveChanges();
        }

        return Task.CompletedTask;
    }
}