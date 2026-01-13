using FnacDarty.TechnicalTest.LibraryManagement.Domain.Entities;
using FnacDarty.TechnicalTest.LibraryManagement.Domain.Repositories;

namespace FnacDarty.TechnicalTest.LibraryManagement.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> Customers;

        public CustomerRepository()
        {
            Customers = new List<Customer>
            {
                new Customer(1,
                "Customer 1",
                new List<BorrowedBook>
                {
                    new BorrowedBook(1, new DateTime(2026,01,01))
                }),
                new Customer(2, "Customer 2",
                new List<BorrowedBook>
                {
                    new BorrowedBook(2, new DateTime(2026,01,05))
                }),
                new Customer(3, "Customer 3",
                new List<BorrowedBook>
                {
                    new BorrowedBook(3, new DateTime(2026,01,10))
                })
            };
        }
        public IReadOnlyCollection<Customer> GetAll()
        {
            return Customers.AsReadOnly();
        }
    }
}
