using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOSReqQueueAPIService.Models
{
	public class SOSReqQueue: TimeStamps
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(1, Int32.MaxValue)]
        public int? Id { get; set; }

        [Required]
        [ForeignKey("SOSRequestId")]
        public int SOSRequestId { get; set; }
        public SOSRequest SOSRequest { get; set; } = null!;

        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [ForeignKey("PoliceId")]
        public int PoliceId { get; set; }
        public User Police { get; set; } = null!;


    }
}

