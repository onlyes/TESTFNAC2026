using FnacDarty.TechnicalTest.LibraryManagement.Domain.Entities;

namespace FnacDarty.TechnicalTest.LibraryManagement.Domain.Repositories
{
    public interface IBookRepository
    {
        IReadOnlyCollection<Book> GetAll();
        void AddBook(Book book);
    }
}
