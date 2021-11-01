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
        [StringLength(PASRConsts.MaxIdentityCodeLength)]
        public string IdentityCode { get; set; }

        [Required]
        [StringLength(PASRConsts.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(PASRConsts.MaxNamesLength)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(14)]
        public string PhoneNumber { get; set; }

        [MaxLength]
        public string LeadNotes { get; set; }

        public string AssignedUserName { get; set; }

        [Required]
        public Lead.LeadPriority Priority { get; set; }

        public Lead.LeadStatus Status { get; set; }

        public ICollection<AddressDto> Addresses { get; set; }

    }
}
