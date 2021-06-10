using PASR.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASR.Web.Models.Leads
{
    public class LeadListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

    }
}
