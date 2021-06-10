using Abp.Application.Services.Dto;
using PASR.Authorization.Users;
using PASR.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Leads.Dto
{
    public class GetLeadForEditOutput
    {
        public LeadEditDto LeadEditDto { get; set; }

        public ListResultDto<UserDto> SDRUsers { get; set; }

    }
}
