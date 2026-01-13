using FnacDarty.TechnicalTest.LibraryManagement.Domain.Entities;
using FnacDarty.TechnicalTest.LibraryManagement.Domain.Repositories;

namespace FnacDarty.TechnicalTest.LibraryManagement.Domain.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IBookRepository _bookRepository;

        public LibraryService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IReadOnlyCollection<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book AddBook(string title, string author)
        {
            var allBooks = _bookRepository.GetAll();
            var id = allBooks.Count == 0 ? 1 : allBooks.Max(b => b.Id) + 1;

            var book = new Book(id, title, true, author);

            _bookRepository.AddBook(book);
            
            return book;
        }

        public IReadOnlyCollection<Customer> GetCustomersWhoBorrowedBooks(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
