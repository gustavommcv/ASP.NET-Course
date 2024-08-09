namespace IActionResultExample.Models
{
    public class Book
    {
        // [FromQuery]
        public int? bookid { get; set; }

        public string? Author { get; set; }

        public override string ToString()
        {
            return $"Book object - Book id {bookid}, Author: {Author}";
        }
    }
}
