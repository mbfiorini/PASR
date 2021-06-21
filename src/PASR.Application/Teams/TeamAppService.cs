using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using PASR.Authorization;
using PASR.Teams.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Teams
{
    [AbpAuthorize(PermissionNames.Pages_Teams)]
    public class TeamAppService : AsyncCrudAppService<Team, TeamDto, int, PagedTeamResultRequestDto, CreateTeamDto, EntityDto, EntityDto>, ITeamAppService
    {
        public TeamAppService(IRepository<Team> repository) : base(repository)
        {
        }

        public async Task DeleteAsync(EntityDto input)
        {
            var team = await Repository.FirstOrDefaultAsync(input.Id);

            if (team == null)
            {
                throw new UserFriendlyException(L("Team Not Found!"));
            }

            await Repository.DeleteAsync(team);
        }

        public async Task<TeamDto> UpdateAsync(TeamDto input)
        {
            CheckUpdatePermission();

            var team = await Repository.FirstOrDefaultAsync(input.Id);

            if (team == null)
            {
                throw new UserFriendlyException(L("Team Not Found!"));
            }

            team = await Repository.UpdateAsync(ObjectMapper.Map<Team>(input));

            return ObjectMapper.Map<TeamDto>(team);
        }

        

        protected override IQueryable<Team> CreateFilteredQuery(PagedTeamResultRequestDto input)
        {
            return base.CreateFilteredQuery(input);
        }

        protected override IQueryable<Team> ApplySorting(IQueryable<Team> query, PagedTeamResultRequestDto input)
        {
            return base.ApplySorting(query, input);
        }
    }
}
