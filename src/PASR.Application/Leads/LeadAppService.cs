﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using PASR.Authorization;
using PASR.Authorization.Roles;
using PASR.Authorization.Users;
using PASR.Leads.Dto;
using PASR.Roles.Dto;
using PASR.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Leads
{
    [AbpAuthorize(PermissionNames.Pages_Leads)]
    public class LeadAppService : AsyncCrudAppService<Lead,LeadDto,int,PagedLeadResultRequestDto,CreateLeadDto,LeadDto>, ILeadAppService
    {
        private readonly UserStore _userStore;

        public LeadAppService(IRepository<Lead> repository, UserStore userStore)
            : base(repository)
        {
            _userStore = userStore;
            DeletePermissionName = "Delete.Lead";
            UpdatePermissionName = "Update.Lead";
        }

        public override async Task<LeadDto> CreateAsync(CreateLeadDto input)
        {
            CheckCreatePermission();

            var lead = ObjectMapper.Map<Lead>(input);

            lead = await Repository.InsertAsync(lead);

            return MapToEntityDto(lead);
        }

        public async Task<ListResultDto<LeadListOutputDto>> GetLeadsByUserAsync(GetAllLeadsByUserInput input)
        {
            var Leads = await Repository
                .GetAllIncluding(l => l.AssignedUser)
                .WhereIf(input.UserId != 0, l => l.AssignedUser.Id == input.UserId)
                .ToListAsync();

            return new ListResultDto<LeadListOutputDto>(ObjectMapper.Map<List<LeadListOutputDto>>(Leads));
        }

        [AbpAuthorize(PermissionNames.Pages_Leads)]
        public override async Task<LeadDto> UpdateAsync(LeadDto input)
        {
            CheckUpdatePermission();

            var lead = await Repository.FirstOrDefaultAsync(input.Id);

            ObjectMapper.Map(input, lead);

            await Repository.UpdateAsync(lead);

            return MapToEntityDto(lead);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var lead = await Repository.FirstOrDefaultAsync(input.Id);

            if (lead == null)
            {
                throw new UserFriendlyException(L("Lead not Found!"));
            }

            await Repository.DeleteAsync(lead);
        }

        protected override IQueryable<Lead> CreateFilteredQuery(PagedLeadResultRequestDto input)
        {
            return Repository.GetAllIncluding(l => l.AssignedUser)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), 
                l => l.Name.Contains(input.Keyword)
                     || l.LastName.Contains(input.Keyword)
                     || l.AssignedUser.FullName.Contains(input.Keyword)
                );
        }

        protected override async Task<Lead> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(l => l.AssignedUser)
                                   .FirstOrDefaultAsync(l => l.Id == id);
        }

        protected override IQueryable<Lead> ApplySorting(IQueryable<Lead> query, PagedLeadResultRequestDto input)
        {
            return query.OrderBy(l => l.Priority);
        }

        public async Task<GetLeadForEditOutput> GetLeadForEdit(EntityDto input)
        {
            var lead = await Repository.FirstOrDefaultAsync(input.Id);
            var leadEditDto = ObjectMapper.Map<LeadEditDto>(lead);
            var SDRsDto = ObjectMapper.Map<ListResultDto<UserDto>>(await _userStore.GetUsersInRoleAsync("SDR"));

            return new GetLeadForEditOutput
            {
                LeadEditDto = leadEditDto,
                SDRUsers = SDRsDto
            };
        }
    }
}
