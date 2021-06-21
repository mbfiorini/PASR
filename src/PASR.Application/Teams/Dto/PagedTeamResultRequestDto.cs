using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Teams.Dto
{
    public class PagedTeamResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
