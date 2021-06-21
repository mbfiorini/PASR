using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PASR.Teams.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Teams
{
    public interface ITeamAppService : IAsyncCrudAppService<TeamDto,int,PagedTeamResultRequestDto,CreateTeamDto,TeamDto,EntityDto,EntityDto>
    {
    }
}
