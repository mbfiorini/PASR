using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PASR.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Leads.Dto
{
    [AutoMap(typeof(Lead))]
    public class LeadDto : EntityDto
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

        [Required]
        [StringLength(PASRConsts.MaxIdentityCodeLength)]
        public string IdentityCode { get; set; }

        [Required]
        [StringLength(PASRConsts.MaxNamesLength)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(PASRConsts.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [MaxLength]
        public string LeadNotes { get; set; }

        public UserDto AssignedUser { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public DateTime CreationTime { get; set; }

        public Lead.LeadPriority Priority { get; set; }

        public ICollection<AddressDto> Addresses { get; set; }
    }
}
