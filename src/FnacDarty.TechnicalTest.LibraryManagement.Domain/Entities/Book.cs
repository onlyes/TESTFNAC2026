namespace FnacDarty.TechnicalTest.LibraryManagement.Domain.Entities
{
    public class Book
    {
        public Book(int id,
            string title,
            bool isAvailable,
            string author)
        {
            Id = id;
            Title = title;
            IsAvailable = isAvailable;
            Author = author;
        }

        public int Id { get; }
        public string Title { get; }
        public bool IsAvailable { get; }
        public string Author { get; }
    }
}
