using System;
using LibraryAPIs.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPIs.Models
{
	public class BookCopy
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public int BooksId { get; set; }

        public bool IsAvailable { get; set; } = true;
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }

        //berkan was here

    }
}

