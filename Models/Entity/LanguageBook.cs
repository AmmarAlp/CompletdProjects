using LibraryAPIs.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPIs.Models
{
    public class LanguageBook
    {

        public string CodesId { get; set; } = "";
        public int BooksId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(CodesId))]
        public Language? language { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}
