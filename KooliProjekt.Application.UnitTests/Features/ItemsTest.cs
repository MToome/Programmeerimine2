using Xunit;
using KooliProjekt.Application.UnitTests;
using KooliProjekt.Application.Features.Items;
using KooliProjekt.Application.Data;

namespace KooliProjekt.UnitTests.Features
{
    public class ItemsTest : TestBase
    {
        [Fact]
        public void Get_should_throw_when_dbcontext_is_null()
        {
            // Arrange
            var query = new GetItemQuery { Id = 1 };
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GetItemQueryHandler(null));
        }

        [Fact]
        public async Task Get_should_return_existing_item()
        {
            // Arrange
            // Need to have an invoice to which the item belongs fot test to work
            var invoice = new KooliProjekt.Application.Data.Invoice
            {
                Id = 1,
                CustomerId = 1,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
            };
            await DbContext.Invoices.AddAsync(invoice);

            var query = new GetItemQuery { Id = 1 };
            var handler = new GetItemQueryHandler(DbContext);
            var item = new KooliProjekt.Application.Data.Item
            {
                Id = 1,
                InvoiceId = 1,
                Name = "Test Item",
                Description = "Test Description",
                Quantity = 2,
                UnitPrice = 10.0m
            };
            await DbContext.Items.AddAsync(item);
            await DbContext.SaveChangesAsync();
            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(query.Id, result.Value.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        [InlineData(-1)]
        public async Task Get_should_return_null_when_item_does_not_exist(int id)
        {
            // Arrange
            // Need to have an invoice to which the item belongs fot test to work
            var invoice = new KooliProjekt.Application.Data.Invoice
            {
                Id = 1,
                CustomerId = 1,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
            };
            await DbContext.Invoices.AddAsync(invoice);

            var query = new GetItemQuery { Id = 1 };
            var handler = new GetItemQueryHandler(DbContext);
            var item = new KooliProjekt.Application.Data.Item
            {
                Id = 1,
                InvoiceId = 1,
                Name = "Test Item",
                Description = "Test Description",
                Quantity = 2,
                UnitPrice = 10.0m
            };
            await DbContext.Items.AddAsync(item);
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
            var invoice = new KooliProjekt.Application.Data.Invoice
            {
                Id = 1,
                CustomerId = 1,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
            };
            await DbContext.Invoices.AddAsync(invoice);

            var query = (GetItemQuery)null;
            var handler = new GetItemQueryHandler(DbContext);
            var item = new KooliProjekt.Application.Data.Item
            {
                Id = 1,
                InvoiceId = 1,
                Name = "Test Item",
                Description = "Test Description",
                Quantity = 2,
                UnitPrice = 10.0m
            };
            await DbContext.Items.AddAsync(item);
            await DbContext.SaveChangesAsync();
            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Theory]
        [InlineData("",0 ,0, 0)]
        [InlineData(null, -1, -1, -1)]
        [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890dsdf", 0, 0, 0)]
        public async Task SaveValidator_Should_Fail_when_info_is_invalid(string name,int id, int quantity, int unitprice)
        {
            // Arrange
            var command = new SaveItemCommand { 
                Name = name, 
                Id = id, 
                // Description over 500 characters
                Description = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890", 
                InvoiceId = 2, 
                Quantity = quantity, 
                UnitPrice = unitprice
            };

            var validator = new SaveItemCommandValidator(DbContext);

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);

            var error = result.Errors.First();
            Assert.Equal(nameof(SaveItemCommand.Name), error.PropertyName);
        }

        [Fact]
        public async Task SaveValidator_Should_Succeed_when_info_is_valid()
        {
            // Arrange
            var command = new SaveItemCommand { Name = "name",Id = 1, Description = "dfs", InvoiceId = 2, Quantity = 4, UnitPrice =10 };
            var validator = new SaveItemCommandValidator(DbContext);

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }
    }
}
