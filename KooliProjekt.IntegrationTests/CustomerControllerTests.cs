using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.IntegrationTests.Helpers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using KooliProjekt.Application.DTO;
using Microsoft.Extensions.DependencyModel;
using System.Net;

namespace KooliProjekt.IntegrationTests
{
    // Run tests sequentially to avoid conflicts with shared resources
    [Collection("Sequential")]
    public class CustomerControllerTests :TestBase
    {
        [Fact]
        public async Task List_should_return_positive_result()
        {
            // Arrange
            var url = "api/customer/list?page=1&pageSize=4"; // Adjust the URL as needed

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<PagedResult<Customer>>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Value);
            Assert.False(response.HasErrors);

        }

        [Fact]
        public async Task Get_should_return_existing_list()
        {
            // Arrange
            var url = "api/customer/get?id=1"; // Adjust the URL as needed

            var customer = new Customer
            {
                Name = "John Doe",
                Address = "123 Main",
                Phone = "555-1234",
                Email = "dsf@test.test",
            };
            await DbContext.AddAsync(customer);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<CustomerDetailsDto>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Value);
            Assert.False(response.HasErrors);
            Assert.Equal(1, response.Value.Id);

        }

        [Fact]
        public async Task Get_should_return_error_for_missing_list()
        {
            // Arrange
            var url = "api/customer/get?id=112"; // Adjust the URL as needed
                      
            // Act
            var response = await Client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            RuntimeAssetGroup.Equals(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
