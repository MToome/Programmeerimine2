using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class Item : Entity // pärineb baasklassist Entity
    {
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public int InvoiceId { get; set; }

        [Required]
        public Invoice Invoice { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Name { get; set; }

        [MaxLength(256)]
        [MinLength(1)]       
        public string Description { get; set; }
        
        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
