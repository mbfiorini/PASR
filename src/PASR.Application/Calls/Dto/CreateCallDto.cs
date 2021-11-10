using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using PASR.Authorization.Users;
using PASR.Calls;
using PASR.Leads;

namespace PASR.Calls.Dto
{
    public class CreateCallDto : EntityDto
    {

        [Required]
        public int LeadId { get; set; }

        public Lead Lead { get; set; }

        public User User { get; set; }

        [Required]
        public DateTime CallStartDateTime { get; set; }

        [Required]
        public DateTime CallEndDateTime { get; set; }
        
        [Required]
        public CallResult callResult { get; set; }

        [Required]
        public bool Scheduled { get; set; }

        [Required]
        public bool Significant { get; set; }
        
        [Required]
        public ResultReason ResultReason { get; set; }

        public bool Intersted { get; set; }
        
        public string CallNotes { get; set; }

        public DateTime NextContact { get; set; }
    }
}