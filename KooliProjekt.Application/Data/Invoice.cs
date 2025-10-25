using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; } = DateTime.Now.AddDays(10);
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<InvoiceLine> Items { get; set; }
    }
}
