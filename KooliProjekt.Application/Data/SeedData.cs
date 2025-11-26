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
        private readonly IList<Invoice> _invoices = new List<Invoice>();
        Random Random = new Random();
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
            for (var l = 1; l <= 10; l++)
            {
                var customer = new Customer
                {
                    Name = $"FirstName{l} " + $"LastName{l}",
                    Address = $"Street {l} Address",
                    City = $"City{l}",
                    Email = $"Email{l}",
                    Phone = $"555-{Random.Next(100, 999)}",
                    Discount = Random.Next(0, 5) * 0.10m
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
                    CustomerId = i,
                    Date = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(30 - i),
                };
                _invoices.Add(invoice);
            }
            _dbContext.Invoices.AddRange(_invoices);
        }

        private void GenerateItems()
        {
            
            foreach (var invoice in _invoices)
            {
                
                for (var j = 1; j <= 5; j++)
                {


                    var item = new Item
                    {
                        InvoiceId = invoice.Id,
                        Name = $"ItemName{j}",
                        Description = $"Description for Item {j}",
                        Quantity = Random.Next(1, 10),
                        UnitPrice = Random.Next(1, 111) * 0.1m
                    };
                    
                    invoice.Items.Add(item);

                }
               
            }

        }
    }
}
   


