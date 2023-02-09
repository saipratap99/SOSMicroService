using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SOSReqQueueAPIService.Models
{
    /**
     * SOSRequest Model contains only required attributes for this Service. 
     */
    public class SOSRequest
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(1, Int32.MaxValue)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public string IncedentDetails { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        public string Pincode { get; set; } = null!;

        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [ForeignKey("PoliceId")]
        public int PoliceId { get; set; }
        public User Police { get; set; } = null!;

        [Required]
        [ForeignKey("PriorityId")]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; } = null!;

        [Required]
        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;

    }
}

