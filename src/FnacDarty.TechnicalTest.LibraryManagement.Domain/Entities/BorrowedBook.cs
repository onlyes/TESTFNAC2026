namespace FnacDarty.TechnicalTest.LibraryManagement.Domain.Entities
{
    public class BorrowedBook
    {
        public BorrowedBook(int bookId, DateTime borrowedAt)
        {
            BookId = bookId;
            BorrowedAt = borrowedAt;
        }

        public int BookId { get; }
        public DateTime BorrowedAt { get; }
    }
}
