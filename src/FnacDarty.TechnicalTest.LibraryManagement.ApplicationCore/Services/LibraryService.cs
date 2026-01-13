using FnacDarty.TechnicalTest.LibraryManagement.Domain.Entities;
using FnacDarty.TechnicalTest.LibraryManagement.Domain.Repositories;

namespace FnacDarty.TechnicalTest.LibraryManagement.Domain.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;

        public LibraryService(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
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
            var allCustomers = _customerRepository.GetAll();

            // Filtrer les clients qui ont emprunté des livres dans la période donnée
            var customersWithBorrowedBooks = allCustomers
                .Where(customer => customer.BorrowedBooks.Any(borrowedBook => 
                    borrowedBook.BorrowedAt >= from && borrowedBook.BorrowedAt <= to))
                .ToList();

            return customersWithBorrowedBooks.AsReadOnly();
        }
    }
}
