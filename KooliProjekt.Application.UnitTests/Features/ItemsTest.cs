using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Customers;
using KooliProjekt.Application.Features.Items;
using KooliProjekt.Application.UnitTests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
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
        public async Task Get_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (GetItemQuery)null;
            var handler = new GetItemQueryHandler(DbContext);

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
            var query = new GetItemQuery { Id = id };
            var handler = new GetItemQueryHandler(GetFaultyDbContext());

            var todoList = new Item { Name = "Test Name" };
            await DbContext.Items.AddAsync(todoList);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
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

        //[Fact]
        //public async Task Get_should_survive_null_request()
        //{
        //    // Arrange
        //    var invoice = new KooliProjekt.Application.Data.Invoice
        //    {
        //        Id = 1,
        //        CustomerId = 1,
        //        Date = DateTime.UtcNow,
        //        DueDate = DateTime.UtcNow.AddDays(30),
        //    };
        //    await DbContext.Invoices.AddAsync(invoice);

        //    var query = (GetItemQuery)null;
        //    var handler = new GetItemQueryHandler(DbContext);
        //    var item = new KooliProjekt.Application.Data.Item
        //    {
        //        Id = 1,
        //        InvoiceId = 1,
        //        Name = "Test Item",
        //        Description = "Test Description",
        //        Quantity = 2,
        //        UnitPrice = 10.0m
        //    };
        //    await DbContext.Items.AddAsync(item);
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
                new ListItemQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (ListItemQuery)null;
            var handler = new ListItemQueryHandler(DbContext);

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
            var query = new ListItemQuery { Page = page, PageSize = pageSize };
            var handler = new ListItemQueryHandler(GetFaultyDbContext());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task List_should_return_page_of_item()
        {
            // Arrange
            var query = new ListItemQuery { Page = 1, PageSize = 5 };
            var handler = new ListItemQueryHandler(DbContext);

            foreach (var i in Enumerable.Range(1, 15))
            {
                var item = new Item { Name = $"Test name {i}" };
                await DbContext.Items.AddAsync(item);
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
        public async Task List_should_return_empty_result_if_item_doesnt_exist()
        {
            // Arrange
            var query = new ListItemQuery { Page = 1, PageSize = 5 };
            var handler = new ListItemQueryHandler(DbContext);

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
                new DeleteItemCommandHandler(null);
            });
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (DeleteItemCommand)null;
            var handler = new DeleteItemCommandHandler(DbContext);

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
            var query = new DeleteItemCommand { Id = id };
            var handler = new DeleteItemCommandHandler(GetFaultyDbContext());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_delete_existing_item()
        {
            // Arrange
            var query = new DeleteItemCommand { Id = 1 };
            var handler = new DeleteItemCommandHandler(DbContext);

            var item = new Item { Name = "Test name" };
            await DbContext.Items.AddAsync(item);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var count = DbContext.Items.Count();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Delete_should_work_with_not_existing_item()
        {
            // Arrange
            var query = new DeleteItemCommand { Id = 1034 };
            var handler = new DeleteItemCommandHandler(DbContext);

            var item = new Item { Name = "Test name" };
            await DbContext.Items.AddAsync(item);
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
                new SaveItemCommandHandler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (SaveItemCommand)null;
            var handler = new SaveItemCommandHandler(DbContext);

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
            var query = new SaveItemCommand { Id = -1 };
            var handler = new SaveItemCommandHandler(GetFaultyDbContext());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_add_new_item()
        {
            // Arrange
            var query = new SaveItemCommand { Id = 0, Name = "Test name" };
            var handler = new SaveItemCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var savedItem = await DbContext.Items.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedItem);
            Assert.Equal(query.Name, savedItem.Name);
        }

        [Fact]
        public async Task Save_should_update_existing_item()
        {
            // Arrange
            var query = new SaveItemCommand { Id = 1,Name = "Test name" };
            var handler = new SaveItemCommandHandler(DbContext);
            var item = new Item { Name = "Old name" };

            await DbContext.Items.AddAsync(item);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var savedItem = await DbContext.Items.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedItem);
            Assert.Equal(query.Name, savedItem.Name);
        }

        [Fact]
        public async Task Save_should_survive_not_existing_item()
        {
            // Arrange
            var query = new SaveItemCommand { Id = 10, Name = "Test name" };
            var handler = new SaveItemCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var savedItem = await DbContext.Items.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Null(savedItem);
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
