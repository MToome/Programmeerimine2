using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTO;
using KooliProjekt.Application.Features.Invoices;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.Extensions.DependencyModel;
using Microsoft.VisualBasic;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class InvoiceControllerTests : TestBase
    {
        [Fact]
        public async Task List_should_return_positive_result()
        {
            // Arrange
            var url = "api/Invoice/list?page=1&pageSize=4"; // Adjust the URL as needed

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<PagedResult<Invoice>>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Value);
            Assert.False(response.HasErrors);

        }

        [Fact]
        public async Task Get_should_return_existing_invoice()
        {
            // Arrange
            var url = "api/Invoice/get?id=1"; // Adjust the URL as needed

            var customer = new Customer
            {
                Name = "John Doe",
                Address = "123 Main",
                Phone = "555-1234",
                Email = "dsf@test.test",
            };

            await DbContext.Customers.AddAsync(customer);

            var invoice = new KooliProjekt.Application.Data.Invoice
            {
                CustomerId = 1,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
            };
            await DbContext.Invoices.AddAsync(invoice);

            var item = new Item
            {
                InvoiceId = 1,
                Name = "Test Invoice",
                Description = "Test Description",
                Quantity = 2,
                UnitPrice = 10.0m
            };
            await DbContext.AddAsync(item);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<InvoiceDetailsDto>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Value);
            Assert.False(response.HasErrors);
            Assert.Equal(1, response.Value.Id);

        }

        [Fact]
        public async Task Get_should_return_error_for_missing_invoice()
        {
            // Arrange
            var url = "api/Invoice/get?id=112"; // Adjust the URL as needed

            // Act
            var response = await Client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            RuntimeAssetGroup.Equals(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_should_remove_existing_invoice()
        {
            // Arrange
            var url = "api/Invoice/delete"; // Adjust the URL as needed

            var customer = new Customer
            {
                Name = "John Doe",
                Address = "123 Main",
                Phone = "555-1234",
                Email = "dsf@test.test",
            };

            await DbContext.Customers.AddAsync(customer);

            var invoice = new KooliProjekt.Application.Data.Invoice
            {
                CustomerId = 1,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
            };
            await DbContext.Invoices.AddAsync(invoice);

            var item = new Item
            {
                InvoiceId = 1,
                Name = "Test Invoice",
                Description = "Test Description",
                Quantity = 2,
                UnitPrice = 10.0m
            };
            await DbContext.AddAsync(item);
            await DbContext.SaveChangesAsync();

            // Act
            var command = new DeleteInvoiceCommand { Id = invoice.Id };
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
        public async Task Delete_should_handle_missing_invoice()
        {
            // Arrange
            var url = "api/Invoice/delete"; // Adjust the URL as needed
            // Act
            var command = new DeleteInvoiceCommand { Id = 112 };
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
        public async Task Save_should_add_new_invoice()
        {
            // Arrange 
            var url = "/api/Invoice/Save"; // Adjust the URL as needed
            var customer = new Customer
            {
                Name = "John Doe",
                Address = "123 Main",
                Phone = "555-1234",
                Email = "dsf@test.test",
            };

            await DbContext.Customers.AddAsync(customer);
            await DbContext.SaveChangesAsync();

            var command = new SaveInvoiceCommand
            {
                Customer = "John Doe",
                CustomerId = 1,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
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
        public async Task Save_should_not_update_missing_invoice()
        {
            // Arrange
            var url = "api/Invoice/Save"; // Adjust the URL as needed
            var command = new SaveInvoiceCommand
            {
                CustomerId = 1,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
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
