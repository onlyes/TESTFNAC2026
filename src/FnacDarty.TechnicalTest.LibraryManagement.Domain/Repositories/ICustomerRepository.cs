using FnacDarty.TechnicalTest.LibraryManagement.Domain.Entities;

namespace FnacDarty.TechnicalTest.LibraryManagement.Domain.Repositories
{
    public interface ICustomerRepository
    {
        IReadOnlyCollection<Customer> GetAll();
    }
}
