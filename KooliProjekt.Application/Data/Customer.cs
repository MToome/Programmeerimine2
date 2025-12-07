using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class Customer : Entity // pärineb baasklassist Entity
    {
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
