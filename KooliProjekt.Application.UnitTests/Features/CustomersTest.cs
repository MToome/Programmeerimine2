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

        [Fact]
        public async Task Get_should_survive_null_request()
        {
            // Arrange
            var query = (GetCustomerQuery)null;
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
            var result = await handler.Handle(null, CancellationToken.None);
            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
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
