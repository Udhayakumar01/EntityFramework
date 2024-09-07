using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Model.Models;

//EF makes use of App Model SnapShot to compare the changes in code and DB
//EF Commands
//To add migration - add-migration MigrationName
//If there are multiple context - add-migration name -context ContextName
//Migration rollback - update-database migrationname
//To Again come to latest migration just run - update-database
//To get all migrations abd to check if they are applied or not - get-migration
//drop-database to delete the database
//Revert
/* Migration Scenarios
 * Add a new class/table in the database
 * add a new property/column 
 * Modilfy existing property/column
 * Delete existing property/column
 * Delete a class/table
 Note - Basically any changes made to a class/entity will trigger a migration in EF Migration COre
*/
// Migrations in EF Core
/*
1. Change/Create Model - Create or Change Existing Model
2. Add Migration - Create a migration after making new changes to know what changes are pushed to the database
3. Apply Migration - Once  Migration is created use update-migration to push migration
*/
// note - __EFMigrationHistory is created by default after applying first migration
// Migrations Folder is also created with the applied migration in that folder
namespace EF_DataAccess.Data
{
    //DbContext is responsible for providing all the logics to interact with the DataBase
    public class AppDBContext : DbContext
    {
        //DBSet is the representation of  table in DB -- Here Books is the Table name
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        //Server and DB Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=.;Database = EFDatabase;TrustServerCertificate=True;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(10,5);
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, ISBN = "100", Title = "Spider Man", Price = 10.23m, Publisher_Id = 1 },
                new Book { Id = 2, ISBN = "101", Title = "Bat Man", Price = 14.00m, Publisher_Id = 2 }
                );
            var bookList = new List<Book>
            {
                new Book { Id = 3, ISBN = "102", Title = "Aqua Man", Price = 33.98m, Publisher_Id = 3 },
                new Book { Id = 4, ISBN = "103", Title = "Super Man", Price = 83.78m, Publisher_Id = 1 },
                new Book { Id = 5, ISBN = "104", Title = "He Man", Price = 08.42m, Publisher_Id = 2 },
                new Book { Id = 6, ISBN = "105", Title = "Hulk", Price = 65.63m, Publisher_Id = 3 }
            };
            modelBuilder.Entity<Book>().HasData(bookList);
            var publisherList = new List<Publisher>
            {
                new Publisher { Publisher_Id = 1, Location = "Chicago", Name = "John Wick"},
                new Publisher { Publisher_Id = 2, Location = "Mythoc", Name = "Harley Quin"},
                new Publisher { Publisher_Id = 3, Location = "France", Name = "Roger Fedrer"}
            };
            modelBuilder.Entity<Publisher>().HasData(publisherList);
        }
    }
}
