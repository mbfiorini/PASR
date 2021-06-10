using Abp.Application.Services.Dto;
using PASR.Localization;
using PASR.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Leads.Dto
{
    public class LeadEditDto : EntityDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [Required]
        [StringLength(14)]
        public string PhoneNumber { get; set; }

        [MaxLength]
        public string LeadNotes { get; set; }

        public UserDto AssignedUser { get; set; }

        public Lead.LeadPriority Priority { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
