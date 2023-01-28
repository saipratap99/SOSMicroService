using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsersAPIService.Constants;

namespace UsersAPIService.Models
{

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

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        [Required]
        public string Password { get; set; }

        [MaxLength(10)]
        public string? Role { get; set; } = Roles.REPORTER;

		public string? Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(6)]
		public string Pincode { get; set; }
	}
}

