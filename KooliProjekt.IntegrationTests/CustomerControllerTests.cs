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
using KooliProjekt.Application.Features.Customers;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.IntegrationTests
{
    // Run tests sequentially to avoid conflicts with shared resources
    [Collection("Sequential")]
    public class CustomerControllerTests : TestBase
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
        public async Task Get_should_return_existing_item()
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

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var url = "api/customer/delete"; // Adjust the URL as needed
            var customer = new Customer
            {
                Name = "John Doe",
                Address = "123 Main",
                Phone = "555-1234",
                Email = "test@test.test"
            };
            await DbContext.AddAsync(customer);
            await DbContext.SaveChangesAsync();

            // Act
            var command = new DeleteCustomerCommand { Id = customer.Id };
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(command)
            };

            var response = await Client.SendAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task Delete_should_handle_missing_customer()
        {
            // Arrange
            var url = "api/customer/delete"; // Adjust the URL as needed
            // Act
            var command = new DeleteCustomerCommand { Id = 112 };
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(command)
            };
            var response = await Client.SendAsync(request);
            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Save_should_add_new_customer()
        {
            // Arrange 
            var url = "/api/customer/Save"; // Adjust the URL as needed
            var command = new SaveCustomerCommand
            {
                Name = "Jane Doe",
                Address = "456 Main",
                Phone = "555-5678",
                Email = "test@test.test",
                City = "Tartu",
                Discount = 0.1m,
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(command)
            };

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var content = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode, content);
        }

        [Fact]
        public async Task Save_should_not_update_missing_customer()
        {
            // Arrange
            var url = "api/customer/save"; // Adjust the URL as needed
            var command = new SaveCustomerCommand
            {
                Id = 112,
                Name = "Jane Doe",
                Address = "456 Main",
                Phone = "555-5678",
                Email = "test@test.test"
            };
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(command)
            };

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}
