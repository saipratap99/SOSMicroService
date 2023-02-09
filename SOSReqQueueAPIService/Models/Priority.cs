using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOSReqQueueAPIService.Models
{
    /**
     * Priority Model contains only required attributes for this Service. 
     */
	public class Priority
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }

        [Required]
        public string PriorityType { get; set; } = null!;
    }
}

