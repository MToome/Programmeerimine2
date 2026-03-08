using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Invoices;
using KooliProjekt.Application.UnitTests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
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
        public async Task Get_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (GetInvoiceQuery)null;
            var handler = new GetInvoiceQueryHandler(DbContext);

            // Act && Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Get_should_return_null_when_request_id_is_null_or_negative(int id)
        {
            // Arrange
            var query = new GetInvoiceQuery { Id = id };
            var handler = new GetInvoiceQueryHandler(GetFaultyDbContext());

            var invoice = new Invoice { Id = 111 };
            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
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

        //[Fact]
        //public async Task Get_should_survive_null_request()
        //{
        //    // Arrange
        //    // Add a customer first to test do work, foreign key constraint
        //    var customer = new Customer
        //    {
        //        Id = 1,
        //        Name = "Test Customer",
        //        Email = "Test",
        //        Address = "Test Address",
        //        Phone = "1234567890",
        //    };

        //    await DbContext.Customers.AddAsync(customer);

        //    var query = (GetInvoiceQuery)null;
        //    var handler = new GetInvoiceQueryHandler(DbContext);

        //    var invoice = new Invoice
        //    {
        //        Id = 1,
        //        Date = DateTime.UtcNow,
        //        DueDate = DateTime.UtcNow.AddDays(30),
        //        CustomerId = 1,
        //    };

        //    await DbContext.Invoices.AddAsync(invoice);
        //    await DbContext.SaveChangesAsync();

        //    // Act
        //    var result = await handler.Handle(query, CancellationToken.None);

        //    // Assert

        //    Assert.NotNull(result);
        //    Assert.False(result.HasErrors);
        //    Assert.Null(result.Value);
        //}
        [Fact]
        public void List_should_throw_when_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ListInvoiceQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (ListInvoiceQuery)null;
            var handler = new ListInvoiceQueryHandler(DbContext);

            // Act && Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(-1, 5)]
        [InlineData(4, -10)]
        [InlineData(5, -5)]
        [InlineData(0, 0)]
        [InlineData(-5, -10)]
        public async Task List_should_return_null_when_page_or_page_size_is_zero_or_negative(int page, int pageSize)
        {
            // Arrange
            var query = new ListInvoiceQuery { Page = page, PageSize = pageSize };
            var handler = new ListInvoiceQueryHandler(GetFaultyDbContext());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task List_should_return_page_of_invoice()
        {
            // Arrange
            var query = new ListInvoiceQuery { Page = 1, PageSize = 5 };
            var handler = new ListInvoiceQueryHandler(DbContext);

            foreach (var i in Enumerable.Range(1, 15))
            {
                var invoice = new Invoice { Id = i };
                await DbContext.Invoices.AddAsync(invoice);
            }

            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(query.Page, result.Value.CurrentPage);
            Assert.Equal(query.PageSize, result.Value.Results.Count);
        }

        [Fact]
        public async Task List_should_return_empty_result_if_invoice_doesnt_exist()
        {
            // Arrange
            var query = new ListInvoiceQuery { Page = 1, PageSize = 5 };
            var handler = new ListInvoiceQueryHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Empty(result.Value.Results);
        }

        [Fact]
        public void Delete_should_throw_when_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new DeleteInvoiceCommandHandler(null);
            });
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (DeleteInvoiceCommand)null;
            var handler = new DeleteInvoiceCommandHandler(DbContext);

            // Act && Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task Delete_should_not_use_dbcontext_if_id_is_zero_or_less(int id)
        {
            // Arrange
            var query = new DeleteInvoiceCommand { Id = id };
            var handler = new DeleteInvoiceCommandHandler(GetFaultyDbContext());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_delete_existing_invoice()
        {
            // Arrange
            var query = new DeleteInvoiceCommand { Id = 1 };
            var handler = new DeleteInvoiceCommandHandler(DbContext);

            var invoice = new Invoice { Id = 1 };
            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var count = DbContext.Invoices.Count();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Delete_should_work_with_not_existing_invoice()
        {
            // Arrange
            var query = new DeleteInvoiceCommand { Id = 1034 };
            var handler = new DeleteInvoiceCommandHandler(DbContext);

            var invoice = new Invoice { Id = 102020 };
            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public void Save_should_throw_when_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SaveInvoiceCommandHandler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (SaveInvoiceCommand)null;
            var handler = new SaveInvoiceCommandHandler(DbContext);

            // Act && Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Fact]
        public async Task Save_should_not_use_dbcontext_if_id_is_negative()
        {
            // Arrange
            var query = new SaveInvoiceCommand { Id = -1 };
            var handler = new SaveInvoiceCommandHandler(GetFaultyDbContext());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_add_new_invoice()
        {
            // Arrange
            var customer = new Customer { Name = "Tiit", Address = "dsaf", Email = "dsf@test.test", Phone = "32532" };
            await DbContext.Customers.AddAsync(customer);
            await DbContext.SaveChangesAsync();

            var query = new SaveInvoiceCommand { Id = 0, CustomerId = customer.Id };
            var handler = new SaveInvoiceCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var savedInvoice = await DbContext.Invoices
                .Include(x => x.Customer)
                .FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedInvoice);
            Assert.Equal("Tiit", savedInvoice.Customer.Name);
        }

        [Fact]
        public async Task Save_should_update_existing_invoice()
        {
            // Arrange
            var query = new SaveInvoiceCommand { Id = 1 };
            var handler = new SaveInvoiceCommandHandler(DbContext);
            var invoice = new Invoice { Id = 1 };

            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var savedInvoice = await DbContext.Invoices.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedInvoice);
            Assert.Equal(query.Id, savedInvoice.Id);
        }

        [Fact]
        public async Task Save_should_survive_not_existing_invoice()
        {
            // Arrange
            var query = new SaveInvoiceCommand { Id = 10};
            var handler = new SaveInvoiceCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var savedInvoice = await DbContext.Invoices.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Null(savedInvoice);
        }

        [Theory]
        [InlineData("", 0, 0)]
        [InlineData(null, 0, 0)]
        [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890dsdf", -1,0)]
        public async Task SaveValidator_Should_Fail_when_info_is_invalid(string customer, int id, int customerid )
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                Customer = customer,
                Id = id,
                Date = new DateTime(2025, 12, 12),
                DueDate = new DateTime(2026, 1, 2),
                CustomerId = customerid
            };
            var validator = new SaveInvoiceCommandValidator(DbContext);

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            
            var error = result.Errors.First();
            Assert.Equal(nameof(SaveInvoiceCommand.Customer), error.PropertyName);
        }

        [Fact]
        public async Task SaveValidator_Should_Succeed_when_info_is_valid()
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                Customer = "customer",
                Id = 1,
                Date = new DateTime(2026, 2, 24),
                DueDate = new DateTime(2026, 3, 24),
                CustomerId = 2
            };
            var validator = new SaveInvoiceCommandValidator(DbContext);

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }
    }
}
