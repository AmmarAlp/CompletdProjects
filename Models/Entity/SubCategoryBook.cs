using LibraryAPIs.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPIs.Models
{
    public class SubCategoryBook
    {

        public short SubId { get; set; }
        public int BooksId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(SubId))]
        public SubCategory? SubCategory { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}
