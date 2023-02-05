﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UsersAPIService.Constants;
using UsersAPIService.Models;

namespace SOSRequestsAPIService.Models
{
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
        public string Address { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        public string Pincode { get; set; } = null!;

        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;

        [ForeignKey("PoliceId")]
        public int? PoliceId { get; set; }
        [JsonIgnore]
        public User? Police { get; set; }

        [Required]
        [ForeignKey("PriorityId")]
        public int PriorityId { get; set; }
        [JsonIgnore]
        public Priority Priority { get; set; } = null!;

        [Required]
        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        [JsonIgnore]
        public Status Status { get; set; } = null!;

        
    }
}

