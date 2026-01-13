using FnacDarty.TechnicalTest.LibraryManagement.Domain.Entities;

namespace FnacDarty.TechnicalTest.LibraryManagement.Domain.Services
{
    public interface ILibraryService
    {
        void AddBook(string Title, string Author);
        IReadOnlyCollection<Book> GetAllBooks();
        IReadOnlyCollection<Customer> GetCustomersWhoBorrowedBooks(DateTime from, DateTime to);
    }
}
