namespace FnacDarty.TechnicalTest.LibraryManagement.Domain.Entities
{
    public class Customer
    {
        public Customer(int id,
            string name,
            IReadOnlyCollection<BorrowedBook> borrowedBooks)
        {
            Id = id;
            Name = name;
            BorrowedBooks = borrowedBooks;
        }

        public int Id { get; }
        public string Name { get; }
        public IReadOnlyCollection<BorrowedBook> BorrowedBooks { get; } = new List<BorrowedBook>();
    }
}
