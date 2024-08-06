using System;
using LibraryAPIs.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LibraryAPIs.Identity;
using System.Text.Json.Serialization;

namespace LibraryAPIs.Models
{
	public class Borrow
	{
        [Key]
		public int Id { get; set; }


        public string? MemberIds { get; set; }
        public int BookCopyId { get; set; }


        

        public DateTime BorrowTime { get; set; }
        public DateTime DueTime { get; set; }
        public DateTime? ReturnTime { get; set; }

       
        public int Amount { get; set; }


        public bool IsReturned { get; set; } = false;

        public string? ResponsibleBy { get; set; }


        public bool Condition { get; set; } = true;

        [JsonIgnore]
        [ForeignKey(nameof(MemberIds))]
        public Member? Member { get; set; }

        

        [JsonIgnore]
        [ForeignKey(nameof(BookCopyId))]
        public BookCopy? BookCopy { get; set; }

        
    }
}

