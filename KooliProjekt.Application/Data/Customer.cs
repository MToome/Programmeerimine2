using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
        
    
        public string City { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Phone { get; set; }
        

        public decimal Discount { get; set; }
    }
}
