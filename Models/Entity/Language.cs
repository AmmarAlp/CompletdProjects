using System;
using LibraryAPIs.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPIs.Models
{
    public class Language
    {
        [Key]
        [Required]
        [StringLength(3,MinimumLength =3)]
        [Column(TypeName ="char(3)")]
        public string Code { get; set; } = "";

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = "";

        public List<Book>? Books { get; set; }
    }
}
