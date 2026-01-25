using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.DTO
{
    public class InvoiceDetailsDto
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int CustomerId { get; set; }
        public CustomerDetailsDto Customer { get; set; }
        public List<ItemDetailDto> Items { get; set; }


    }
}
