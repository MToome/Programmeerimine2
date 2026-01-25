using KooliProjekt.Application.Features.Customers;
using Xunit;
using KooliProjekt.Application.UnitTests;
using KooliProjekt.Application.Data;

namespace KooliProjekt.UnitTests.Features
{
    public class CustomersTest : TestBase
    {
        [Fact]
        public void Get_should_throw_when_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new GetCustomerQueryHandler(null));
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
    }
}
