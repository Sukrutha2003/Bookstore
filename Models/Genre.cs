namespace Bookstore.Models
{
    public class Genre
    {
        public string GenreId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}