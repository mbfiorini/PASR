using Abp.Application.Services.Dto;
using PASR.Authorization.Users;
using PASR.Leads;
using PASR.Leads.Dto;
using PASR.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Calls.Dto
{
    public class CallDto : EntityDto
    {

        public UserDto User { get; set; }

        public LeadDto Lead { get; set; }

        [Required]
        public DateTime CallStartDateTime { get; set; }

        [Required]
        public DateTime CallEndDateTime { get; set; }
        
        [Required]
        public CallResult CallResult { get; set; }

        [Required]
        public ResultReason ResultReason { get; set; }

        public string CallNotes { get; set; }

        public bool Intersted { get; set; }

        public string Duration { get; set; }
        
    }    
}
