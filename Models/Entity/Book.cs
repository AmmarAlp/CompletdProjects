using LibraryAPIs.Model;
using System;
using Microsoft.AspNetCore.Components.Routing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LibraryAPIs.Models;

namespace LibraryAPIs.Model
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(13, MinimumLength =10)]
        [Column(TypeName ="varchar(13)")]
        public string? ISBN { get; set; }

        [Required]
        [StringLength(2000)]
        public string Title { get; set; } = "";

        [Range(1,short.MaxValue)]
        public short PageCount { get; set; }

        [Range(-4000,2100)]
        public short PublishingYear { get; set; }

        [StringLength(5000)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue)]
        public int PrintCount { get; set; }

        public bool Banned { get; set; }

        [Range(0, 5)]
        public float  Rating { get; set; }


        public List<AuthorBook>? AuthorBooks { get; set; }


        public List<LanguageBook>? LanguageBooks { get; set; }

        public List<SubCategoryBook>? SubCategoryBooks { get; set; }

        public List<BookCopy>? BookCopies { get; set; }

        [NotMapped]
        public List<long>? AuthorsIds { get; set; }

        [NotMapped]
        public List<string>? LanguagesCodes { get; set; }

        [NotMapped]
        public List<short>? SubCategoriesIds { get; set; }

        [NotMapped]
        public string? Image { get; set; }

        public int PublisherId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(PublisherId))]
        public Publisher? Publisher { get; set; }

        public List<SubCategory>? SubCategories  { get; set; }
        public List<Language>? Languages { get; set; }

        [StringLength(6, MinimumLength = 3)]
        [Column(TypeName = "varchar(6)")]
        public string LocationShelf { get; set; } = "";

        [JsonIgnore]
        [ForeignKey(nameof(LocationShelf))]
        public Location? Location { get; set; }


    }
}
