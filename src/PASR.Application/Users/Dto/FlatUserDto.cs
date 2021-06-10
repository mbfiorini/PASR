using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using PASR.Teams.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Users.Dto
{
    public class FlatUserDto : EntityDto
    {
        public string FullName { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

    }
}
