using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Range(0.01, 1000.00)]
        public double Price { get; set; }

        [Required]
        public string GenreId { get; set; } = string.Empty;
        public Genre Genre { get; set; } = null!;

        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}