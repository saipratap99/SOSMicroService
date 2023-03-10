using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIRAPIService.Models
{
    /**
     * User Model contains only required attributes for this Service. 
     */
    public class User
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(1, Int32.MaxValue)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public string Name { get; set; } = null!;

        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        public string City { get; set; }

        [MaxLength(10)]
        public string? Role { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        public string Pincode { get; set; }
    }
}

