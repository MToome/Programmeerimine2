using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Invoices;
using KooliProjekt.Application.UnitTests;
using Xunit;

namespace KooliProjekt.UnitTests.Features
{
    public class InvoicesTest : TestBase
    {
        [Fact]
        public void Get_should_throw_when_dbcontext_is_null()
        {
            // Arrange
            var query = new GetInvoiceQuery { Id = 1 };
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GetInvoiceQueryHandler(null));
        }


        [Fact]
        public async Task Get_should_return_existing_invoice()
        {
            // Arrange
            // Add a customer first to test do work, foreign key constraint
            var customer = new Customer
            {
                Id = 1,
                Name = "Test Customer",
                Email = "Test",
                Address = "Test Address",
                Phone = "1234567890",
            };

            await DbContext.Customers.AddAsync(customer);

            var query = new GetInvoiceQuery { Id = 1 };
            var handler = new GetInvoiceQueryHandler(DbContext);

            var invoice = new Invoice
            {
                Id = 1,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                CustomerId = 1, 
            };

            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(101)]
        public async Task Get_should_return_null_when_invoice_does_not_exist(int id)
        {
            // Arrange
            // Add a customer first to test do work, foreign key constraint
            var customer = new Customer
            {
                Id = 1,
                Name = "Test Customer",
                Email = "Test",
                Address = "Test Address",
                Phone = "1234567890",
            };

            await DbContext.Customers.AddAsync(customer);

            var query = new GetInvoiceQuery { Id = 1 };
            var handler = new GetInvoiceQueryHandler(DbContext);

            var invoice = new Invoice
            {
                Id = 1,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                CustomerId = 1,
            };

            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task Get_should_survive_null_request()
        {
            // Arrange
            // Add a customer first to test do work, foreign key constraint
            var customer = new Customer
            {
                Id = 1,
                Name = "Test Customer",
                Email = "Test",
                Address = "Test Address",
                Phone = "1234567890",
            };

            await DbContext.Customers.AddAsync(customer);

            var query = (GetInvoiceQuery)null;
            var handler = new GetInvoiceQueryHandler(DbContext);

            var invoice = new Invoice
            {
                Id = 1,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                CustomerId = 1,
            };

            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }
    }
}
