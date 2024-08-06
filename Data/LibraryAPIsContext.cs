using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryAPIs.Models;
using LibraryAPIs.Model;
using LibraryAPIs.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibraryAPIs.Data
{
    public class LibraryAPIsContext : IdentityDbContext<ApplicationUser>
    {
    
        public LibraryAPIsContext (DbContextOptions<LibraryAPIsContext> options)
            : base(options)
        {
        }


        public DbSet<LibraryAPIs.Models.Location> Location { get; set; } = default!;


        public DbSet<LibraryAPIs.Models.Language>? Language { get; set; }


        public DbSet<LibraryAPIs.Models.Author>? Author { get; set; }


        public DbSet<LibraryAPIs.Model.Book>? Book { get; set; }


        public DbSet<LibraryAPIs.Models.Category>? Category { get; set; }


        public DbSet<LibraryAPIs.Models.SubCategory>? SubCategory { get; set; }


        public DbSet<LibraryAPIs.Models.Publisher>? Publisher { get; set; }


        public DbSet<AuthorBook>? AuthorBook { get; set; }
        public DbSet<LanguageBook>? LanguageBook { get; set; }

        public DbSet<SubCategoryBook>? SubCategoryBook { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Borrow>().HasOne(x => x.Member).WithMany().OnDelete(DeleteBehavior.Restrict);
  
            
            modelBuilder.Entity<Borrow>().HasOne(y => y.Member).WithMany().OnDelete(DeleteBehavior.Restrict);
         

           modelBuilder.Entity<AuthorBook>().HasKey(a => new {a.AuthorsId, a.BooksId});
        

            Microsoft.EntityFrameworkCore.Metadata.Builders.KeyBuilder keyBuilder = modelBuilder.Entity<LanguageBook>().HasKey(l => new { l.CodesId, l.BooksId });

            modelBuilder.Entity<SubCategoryBook>().HasKey(s => new { s.SubId, s.BooksId });
        }


        public DbSet<LibraryAPIs.Identity.Employee>? Employee { get; set; }


        public DbSet<LibraryAPIs.Identity.Member>? Member { get; set; }


        public DbSet<LibraryAPIs.Models.Borrow>? Borrow { get; set; }


        


        public DbSet<LibraryAPIs.Models.BookCopy>? BookCopy { get; set; }
    }
}
