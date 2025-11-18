using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class SeedData
    {
        private readonly ApplicationDbContext _dbContext;

        // 15.11.2023 - Lisatud SeedData klass andmebaasi algandmete genereerimiseks

        public SeedData(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        // Meetod andmebaasi algandmete genereerimiseks
        public void Generate()
        {
            // Kontrollime, kas andmebaasis on juba andmeid
            // Kui on, siis ei tee midagi
            if (_dbContext.Customers.Any())
            {
                return;
            }

            GenerateCustomers();
            GenerateInvoices();
            GenerateItems();

            _dbContext.SaveChanges();
        }

        private void GenerateCustomers()
        {
            for (var i = 1; i <= 10; i++)
            {
                var customer = new Customer
                {
                    Name = $"FirstName{i+1} " + "LastName{i}",
                    Address = $"Street {i+1} Address",
                    City = $"City{i+1}",
                    Email = $"Email{i + 1}",
                    Phone = $"555-010{i}",
                    Discount = i * 0.01m
                };

                _dbContext.Customers.Add(customer);
            }
        }

        private void GenerateInvoices()
        {
            for (var i = 1; i <= 5; i++)
            {
                var invoice = new Invoice
                {
                    CustomerId = i+1,
                    Date = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(30 - i),
                };
                _dbContext.Invoices.Add(invoice);
            }
        }

        private void GenerateItems()
        {
            for (var i = 1; i < 5; i++)
            {
                var item = new Item
                {
                    InvoiceId = i+1,
                    Name = $"ItemName{i+1}",
                    Description = $"Description for Item {i + 1}",
                    Quantity = i+1 * 2,
                    UnitPrice = i+1 * 5.00m
                };
                _dbContext.Items.Add(item);
            }
        }

    }
}
