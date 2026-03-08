using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Customers;
using Xunit;
using Microsoft.EntityFrameworkCore;
{
    
}

namespace KooliProjekt.Application.UnitTests.Features
{
    public class CustomersTest : TestBase
    {
        [Fact]
        public void Get_should_throw_when_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new GetCustomerQueryHandler(null));
        }

        [Fact]
        public async Task Get_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (GetCustomerQuery)null;
            var handler = new GetCustomerQueryHandler(DbContext);

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
            var query = new GetCustomerQuery { Id = id };
            var handler = new GetCustomerQueryHandler(GetFaultyDbContext());

            var customer = new Customer { Name = "Test", Address = "dsaf", Email = "dsf@test.test", Phone = "32532" };
            await DbContext.Customers.AddAsync(customer);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_Should_Return_Existing_Customer()
        {
            // Arrange
            var query = new GetCustomerQuery { Id = 1 };
            var handler = new GetCustomerQueryHandler(DbContext);

            var customer = new Customer
            {
                Id = 1,
                Name = "Test",
                Address = "Test Address",
                City = "Test City",
                Email = "Test Email",
                Phone = "5555555",
                Discount = 0
            };

            await DbContext.Customers.AddAsync(customer);
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
        [InlineData(-3)]
        [InlineData(101)]
        public async Task Get_Should_Return_null_when_Customer_does_not_exist(int id)
        {
            // Arrange
            var query = new GetCustomerQuery { Id = id };
            var handler = new GetCustomerQueryHandler(DbContext);

            var customer = new Customer
            {
                Id = 1,
                Name = "Test",
                Address = "Test Address",
                City = "Test City",
                Email = "Test Email",
                Phone = "5555555",
                Discount = 0
            };

            await DbContext.Customers.AddAsync(customer);
            await DbContext.SaveChangesAsync();

            // Act

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        //[Fact]
        //public async Task Get_should_survive_null_request()
        //{
        //    // Arrange
        //    var query = (GetCustomerQuery)null;
        //    var handler = new GetCustomerQueryHandler(DbContext);

        //    var customer = new Customer
        //    {
        //        Id = 1,
        //        Name = "Test",
        //        Address = "Test Address",
        //        City = "Test City",
        //        Email = "Test Email",
        //        Phone = "5555555",
        //        Discount = 0
        //    };
        //    await DbContext.Customers.AddAsync(customer);
        //    await DbContext.SaveChangesAsync();
        //    // Act
        //    var result = await handler.Handle(null, CancellationToken.None);
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
                new ListCustomerQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (ListCustomerQuery)null;
            var handler = new ListCustomerQueryHandler(DbContext);

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
            var query = new ListCustomerQuery { Page = page, PageSize = pageSize };
            var handler = new ListCustomerQueryHandler(GetFaultyDbContext());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task List_should_return_page_of_customer()
        {
            // Arrange
            var query = new ListCustomerQuery { Page = 1, PageSize = 5 };
            var handler = new ListCustomerQueryHandler(DbContext);

            foreach (var i in Enumerable.Range(1, 15))
            {
                var customer = new Customer { 
                    Name = $"Test client {i}",
                    Address = "Test Address",
                    City = "Test City",
                    Email = "Test Email",
                    Phone = "5555555",
                    Discount = 0
                };
                await DbContext.Customers.AddAsync(customer);
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
        public async Task List_should_return_empty_result_if_todo_lists_doesnt_exist()
        {
            // Arrange
            var query = new ListCustomerQuery { Page = 1, PageSize = 5 };
            var handler = new ListCustomerQueryHandler(DbContext);

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
                new DeleteCustomerCommandHandler(null);
            });
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (DeleteCustomerCommand)null;
            var handler = new DeleteCustomerCommandHandler(DbContext);

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
            var query = new DeleteCustomerCommand { Id = id };
            var handler = new DeleteCustomerCommandHandler(GetFaultyDbContext());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_delete_existing_Customer()
        {
            // Arrange
            var query = new DeleteCustomerCommand { Id = 1 };
            var handler = new DeleteCustomerCommandHandler(DbContext);

            var customer = new Customer { Name = "Test name", Address = "Test aadress",  Email = "test@test.test", Phone = "+test" };
            await DbContext.Customers.AddAsync(customer);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var count = DbContext.Customers.Count();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Delete_should_work_with_not_existing_list()
        {
            // Arrange
            var query = new DeleteCustomerCommand { Id = 1034 };
            var handler = new DeleteCustomerCommandHandler(DbContext);

            var customer = new Customer { Name = "Test name", Address = "Test aadress", Email = "test@test.test", Phone = "+test" };
            await DbContext.Customers.AddAsync(customer);
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
                new SaveCustomerCommandHandler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (SaveCustomerCommand)null;
            var handler = new SaveCustomerCommandHandler(DbContext);

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
            var query = new SaveCustomerCommand { Id = -1 };
            var handler = new SaveCustomerCommandHandler(GetFaultyDbContext());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_add_new_todo_list()
        {
            // Arrange
            var query = new SaveCustomerCommand { Id = 0, Name = "Test name", Address = "Test aadress", Email = "test@test.test", Phone = "+test" };
            var handler = new SaveCustomerCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var savedCustomer = await DbContext.Customers.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedCustomer);
            Assert.Equal(query.Name, savedCustomer.Name);
        }

        [Fact]
        public async Task Save_should_update_existing_todo_list()
        {
            // Arrange
            var query = new SaveCustomerCommand { Id = 1, Name = "Test name", Address = "Test aadress", Email = "test@test.test", Phone = "+test" };
            var handler = new SaveCustomerCommandHandler(DbContext);
            var customer = new Customer { Name = "Old name", Address = "Test old aadress", Email = "old@test.test", Phone = "+ooldtest" };

            await DbContext.Customers.AddAsync(customer);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var savedCustomer = await DbContext.Customers.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedCustomer);
            Assert.Equal(query.Name, savedCustomer.Name);
        }

        [Fact]
        public async Task Save_should_survive_not_existing_list()
        {
            // Arrange
            var query = new SaveCustomerCommand {Id = 10, Name = "Test name", Address = "Test aadress", Email = "test@test.test", Phone = "+test" };
            var handler = new SaveCustomerCommandHandler(DbContext);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var savedCustomer = await DbContext.Customers.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Null(savedCustomer);
        }

        [Theory]
        [InlineData("", 0, "" )]
        [InlineData(null, -1, "invalidemail")]
        [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890dsdf", 0, null)]
        public async Task SaveValidator_Should_Fail_when_info_is_invalid(string name, int id, string email)
        {
            // Arrange
            var command = new SaveCustomerCommand
            {
                Id = id,
                Name = name,
                Email = email,
                Address = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890dsdf1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890dsdf",
                Phone = "123456789012345678901"
            };
            var validator = new SaveCustomerCommandValidator(DbContext);

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);

            var error = result.Errors.First();
            Assert.Equal(nameof(SaveCustomerCommand.Name), error.PropertyName);
        }

        [Fact]
        public async Task SaveValidator_Should_Succeed_when_info_is_valid()
        {
            // Arrange
            var command = new SaveCustomerCommand
            {
                Id = 1,
                Name = "name",
                Email = "email@test.test",
                Address = "1234567890",
                Phone = "1234567890"
            };
            var validator = new SaveCustomerCommandValidator(DbContext);

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }
    }
}
