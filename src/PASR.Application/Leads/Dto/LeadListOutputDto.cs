using Abp.Application.Services.Dto;
using PASR.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Leads.Dto
{
    public class LeadListOutputDto : EntityDto
    {
        public string FullName { get; set; }

        public Lead.LeadPriority Priority { get; set; }

        public UserDto AssignedUser { get; set; }

    }
}
