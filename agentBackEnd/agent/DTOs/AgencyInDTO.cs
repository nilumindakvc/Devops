using agent.entityClasses;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class AgencyInDTO
    {
        [Required]
        [StringLength(100)]
        public string AgencyName { get; set; }

        [StringLength(50)]
        [Required]
        public string LicenseNumber { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public string Password { get; set; }
    }
}