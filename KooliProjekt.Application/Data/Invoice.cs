using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class Invoice : Entity // pärineb baasklassist Entity
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(10);

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public Customer Customer { get; set; }

        [Required]
        public List<Item> Items { get; set; }

        // null-viidet(NullReference) vältimiseks konstruktori kaudu
        public Invoice()
        {
            Items = new List<Item>();
        }
    }
}
