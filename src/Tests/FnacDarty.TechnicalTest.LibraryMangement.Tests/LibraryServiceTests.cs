using FnacDarty.TechnicalTest.LibraryManagement.Domain.Entities;
using FnacDarty.TechnicalTest.LibraryManagement.Domain.Repositories;
using FnacDarty.TechnicalTest.LibraryManagement.Domain.Services;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace FnacDarty.TechnicalTest.LibraryMangement.Tests.Services
{
    [TestFixture]
    public class LibraryServiceTests
    {
        private Mock<IBookRepository> _mockBookRepository;
        private Mock<ICustomerRepository> _mockCustomerRepository;
        private LibraryService _libraryService;

        [SetUp]
        public void SetUp()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _libraryService = new LibraryService(_mockBookRepository.Object, _mockCustomerRepository.Object);
        }

        #region GetCustomersWhoBorrowedBooks Tests

        [Test]
        public void GetCustomersWhoBorrowedBooks_ShouldReturnCustomersWithinDateRange()
        {
            // Arrange
            var from = new DateTime(2026, 1, 1);
            var to = new DateTime(2026, 1, 31);

            var customers = new List<Customer>
            {
                new Customer(1, "Alice Martin", new List<BorrowedBook>
                {
                    new BorrowedBook(1, new DateTime(2026, 1, 5)),
                    new BorrowedBook(2, new DateTime(2026, 1, 15))
                }),
                new Customer(2, "Bob Dupont", new List<BorrowedBook>
                {
                    new BorrowedBook(3, new DateTime(2025, 12, 20)) // En dehors de la période
                }),
                new Customer(3, "Charlie Leroy", new List<BorrowedBook>
                {
                    new BorrowedBook(4, new DateTime(2026, 1, 10))
                })
            };

            _mockCustomerRepository.Setup(repo => repo.GetAll()).Returns(customers);

            // Act
            var result = _libraryService.GetCustomersWhoBorrowedBooks(from, to);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(c => c.Name == "Alice Martin");
            result.Should().Contain(c => c.Name == "Charlie Leroy");
            result.Should().NotContain(c => c.Name == "Bob Dupont");
        }

        [Test]
        public void GetCustomersWhoBorrowedBooks_ShouldReturnEmptyList_WhenNoCustomersInDateRange()
        {
            // Arrange
            var from = new DateTime(2026, 2, 1);
            var to = new DateTime(2026, 2, 28);

            var customers = new List<Customer>
            {
                new Customer(1, "Alice Martin", new List<BorrowedBook>
                {
                    new BorrowedBook(1, new DateTime(2026, 1, 5))
                }),
                new Customer(2, "Bob Dupont", new List<BorrowedBook>
                {
                    new BorrowedBook(2, new DateTime(2025, 12, 20))
                })
            };

            _mockCustomerRepository.Setup(repo => repo.GetAll()).Returns(customers);

            // Act
            var result = _libraryService.GetCustomersWhoBorrowedBooks(from, to);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void GetCustomersWhoBorrowedBooks_ShouldExcludeCustomersWithNoBorrowedBooks()
        {
            // Arrange
            var from = new DateTime(2026, 1, 1);
            var to = new DateTime(2026, 1, 31);

            var customers = new List<Customer>
            {
                new Customer(1, "Alice Martin", new List<BorrowedBook>
                {
                    new BorrowedBook(1, new DateTime(2026, 1, 5))
                }),
                new Customer(2, "Bob Dupont", new List<BorrowedBook>()), // Aucun livre emprunté
                new Customer(3, "Charlie Leroy", new List<BorrowedBook>
                {
                    new BorrowedBook(2, new DateTime(2026, 1, 10))
                })
            };

            _mockCustomerRepository.Setup(repo => repo.GetAll()).Returns(customers);

            // Act
            var result = _libraryService.GetCustomersWhoBorrowedBooks(from, to);

            // Assert
            result.Should().HaveCount(2);
            result.Should().NotContain(c => c.Name == "Bob Dupont");
        }

        [Test]
        public void GetCustomersWhoBorrowedBooks_ShouldIncludeCustomerOnBoundaryDate_From()
        {
            // Arrange - Test de la date limite inférieure (from)
            var from = new DateTime(2026, 1, 1);
            var to = new DateTime(2026, 1, 31);

            var customers = new List<Customer>
            {
                new Customer(1, "Alice Martin", new List<BorrowedBook>
                {
                    new BorrowedBook(1, from) // Exactement à la date de début
                })
            };

            _mockCustomerRepository.Setup(repo => repo.GetAll()).Returns(customers);

            // Act
            var result = _libraryService.GetCustomersWhoBorrowedBooks(from, to);

            // Assert
            result.Should().HaveCount(1);
            result.Should().Contain(c => c.Name == "Alice Martin");
        }

        [Test]
        public void GetCustomersWhoBorrowedBooks_ShouldIncludeCustomerOnBoundaryDate_To()
        {
            // Arrange - Test de la date limite supérieure (to)
            var from = new DateTime(2026, 1, 1);
            var to = new DateTime(2026, 1, 31);

            var customers = new List<Customer>
            {
                new Customer(1, "Bob Dupont", new List<BorrowedBook>
                {
                    new BorrowedBook(1, to) // Exactement à la date de fin
                })
            };

            _mockCustomerRepository.Setup(repo => repo.GetAll()).Returns(customers);

            // Act
            var result = _libraryService.GetCustomersWhoBorrowedBooks(from, to);

            // Assert
            result.Should().HaveCount(1);
            result.Should().Contain(c => c.Name == "Bob Dupont");
        }

        [Test]
        public void GetCustomersWhoBorrowedBooks_ShouldReturnCustomerOnlyOnce_EvenWithMultipleBorrowings()
        {
            // Arrange - Un client avec plusieurs emprunts dans la période
            var from = new DateTime(2026, 1, 1);
            var to = new DateTime(2026, 1, 31);

            var customers = new List<Customer>
            {
                new Customer(1, "Alice Martin", new List<BorrowedBook>
                {
                    new BorrowedBook(1, new DateTime(2026, 1, 5)),
                    new BorrowedBook(2, new DateTime(2026, 1, 10)),
                    new BorrowedBook(3, new DateTime(2026, 1, 20))
                })
            };

            _mockCustomerRepository.Setup(repo => repo.GetAll()).Returns(customers);

            // Act
            var result = _libraryService.GetCustomersWhoBorrowedBooks(from, to);

            // Assert
            result.Should().HaveCount(1);
            result.First().BorrowedBooks.Should().HaveCount(3);
        }

        [Test]
        public void GetCustomersWhoBorrowedBooks_ShouldReturnReadOnlyCollection()
        {
            // Arrange
            var from = new DateTime(2026, 1, 1);
            var to = new DateTime(2026, 1, 31);

            var customers = new List<Customer>
            {
                new Customer(1, "Alice Martin", new List<BorrowedBook>
                {
                    new BorrowedBook(1, new DateTime(2026, 1, 5))
                })
            };

            _mockCustomerRepository.Setup(repo => repo.GetAll()).Returns(customers);

            // Act
            var result = _libraryService.GetCustomersWhoBorrowedBooks(from, to);

            // Assert
            result.Should().BeAssignableTo<IReadOnlyCollection<Customer>>();
        }

        #endregion
    }
}