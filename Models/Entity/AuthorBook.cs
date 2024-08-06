using LibraryAPIs.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPIs.Models
{
    public class AuthorBook
    {

        public long AuthorsId { get; set; }
        public int BooksId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(AuthorsId))]
        public Author? Author { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}
