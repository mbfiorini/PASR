using PASR.Teams.Dto;
using PASR.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASR.Web.Models.Teams
{
    public class TeamListViewModel
    {
        public IReadOnlyList<TeamDto> Teams { get; set; }
    }
}
