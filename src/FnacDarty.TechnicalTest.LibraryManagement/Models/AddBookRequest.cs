namespace FnacDarty.TechnicalTest.LibraryManagement.Models
{
    public class AddBookRequest
    {
        public AddBookRequest(string title,
            string author)
        {
            Title = title;
            Author = author;
        }

        public string Title { get; }
        public string Author { get; }
    }
}
