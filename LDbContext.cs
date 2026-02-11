using LMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMS.Data
{
    public class LDbContext : DbContext
    {
        public LDbContext(DbContextOptions<LDbContext> options) : base(options)
        {

        }
        // Seed initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<Category>().HasData(
               new Category
               { CategoryId = 1,
                   Name = "Fiction"
               },
               new Category
               {
                   CategoryId = 2,
                   Name = "Technology"
               },
               new Category
               {
                   CategoryId = 3,
                   Name = "Science"
               },
               new Category
               {
                   CategoryId = 4,
                   Name = "History"
               },
               new Category
               {
                   CategoryId = 5,
                   Name = "Programmong"
               }

             );
        
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt and David Thomas",
                    CoverImagePath = "~/Covers/b7.png",
                    
                    CategoryId = 5,
                    ISBN = "978-0201616224",
                    PublishedDate = new DateTime(2021, 10, 30),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 2,
                    
                    Title = "Design Pattern using C#",
                    Author = "Robert C. Martin",
                    CategoryId = 5,
                    ISBN = "978-0132350884",
                    PublishedDate = new DateTime(2023, 8, 1),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 3,
                   
                    Title = "Mastering ASP.NET Core",
                    Author = "Pranaya Kumar Rout",
                    CategoryId = 5, 
                    ISBN = "978-0451616235",
                    PublishedDate = new DateTime(2022, 11, 22),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 4,

                    Title = "SQL Server with DBA",
                    Author = "Rakesh Kumat",
                    CategoryId = 5,
                     
                    ISBN = "978-4562350123",
                    PublishedDate = new DateTime(2020, 8, 15),
                    IsAvailable = true
                },


                new Book
                {
                         BookId = 5,
                        
                         Title = "Clean Code",
                         Author = "Robert C. Martin",
                    CoverImagePath = "~/Covers/b6.png",
                    CategoryId = 5,
                         ISBN = "978-0132350884",
                         PublishedDate = new DateTime(2019, 5, 10),
                         IsAvailable = true
                },
                new Book
                {
                    BookId = 6,
                  
                    Title = "Refactoring",
                    Author = "Martin Fowler",
                    CoverImagePath = "~/Covers/b5.png",
                    CategoryId = 5,
                    ISBN = "978-0201485677",
                    PublishedDate = new DateTime(2018, 3, 20),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 7,
                 
                    Title = "Head First Design Patterns",
                    Author = "Eric Freeman",
                    CoverImagePath = "~/Covers/b4.png",
                    CategoryId = 5,
                    ISBN = "978-0596007126",
                    PublishedDate = new DateTime(2020, 7, 15),
                    IsAvailable = true
                },
                new Book
                {
                     BookId = 8,
                  
                    Title = "C# in Depth",
                     Author = "Jon Skeet",
                    CoverImagePath = "~/Covers/b1.png",
                    CategoryId = 5,
                    ISBN = "978-1617294532",
                    PublishedDate = new DateTime(2021, 2, 5),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 9,
                    
                    Title = "Pro ASP.NET Core MVC",
                    Author = "Adam Freeman",
                    CoverImagePath = "~/Covers/b2.png",
                    CategoryId = 5,
                    ISBN = "978-1484254394",
                    PublishedDate = new DateTime(2022, 9, 1),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 10,
                    
                    Title = "You Don’t Know JS",
                    Author = "Kyle Simpson",
                    CoverImagePath = "~/Covers/b3.png",
                    CategoryId = 5,
                    ISBN = "978-1491904244",
                    PublishedDate = new DateTime(2017, 12, 18),
                    IsAvailable = true
                }


            );
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<Category> Categories { get; set; }



    }
}
