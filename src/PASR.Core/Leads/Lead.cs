using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;
using PASR.Authorization.Users;
using PASR.Calls;
using PASR.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Leads
{
    public class Lead : FullAuditedEntity<int>
    {
        protected Lead() { }

        public Lead(string name, string lastName, string phoneNumber, string cgc)
        {
            Name = name;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            IdentityCode = cgc;
        }

        [Required]
        [StringLength(PASRConsts.MaxIdentityCodeLength)]
        public string IdentityCode { get; set; }

        [Required]
        [StringLength(PASRConsts.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(PASRConsts.MaxNamesLength)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(PASRConsts.MaxNamesLength)]
        public string LastName { get; set; }
        
        public User AssignedUser { get; set; }

        [Required]
        [StringLength(14)]
        public string PhoneNumber { get; set; }

        public ICollection<Call> Calls { get; set; }

        [MaxLength]
        public string LeadNotes { get; set; }

        public ICollection<Address> Addresses { get; set; }

        [Required]
        [EnumDataType(typeof(LeadPriority))]
        public LeadPriority Priority { get; set; }

        public enum LeadPriority
        {
            Max,
            Normal,
            Min
        }

    }
}
