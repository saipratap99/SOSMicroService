using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOSReqQueueAPIService.Models
{
	public class FIR
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(1, Int32.MaxValue)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public string Details { get; set; } = null!;

        public string? Address { get; set; }

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string Pincode { get; set; } = null!;

        [Required]
        public DateTime incedentDateTime { get; set; }

        [Required]
        [ForeignKey("SOSRequestId")]
        public int SOSRequestId { get; set; }
        public SOSRequest SOSRequest { get; set; } = null!;

        [Required]
        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;

    }
}

