using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Validation;
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
            this.LocalizationSourceName = PASRConsts.LocalizationSourceName; 
            DeletePermissionName = PermissionNames.Update_Leads;
            UpdatePermissionName = PermissionNames.Update_Leads;
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
                .WhereIf(input.UserId > 0, l => l.AssignedUser.Id == input.UserId)
                .ToListAsync();

            return new ListResultDto<LeadListOutputDto>(ObjectMapper.Map<List<LeadListOutputDto>>(Leads));
        }

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

        public async Task AssignToUserAsync(AssignToUserDto input)
        {
            try
            {
                CheckUpdatePermission();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            var lead = await Repository
                .FirstOrDefaultAsync(input.Id);

            if (lead == null)
            {
                throw new UserFriendlyException(L("Lead not Found!"));
            }

            try
            {
                var user = await _userStore.FindByIdAsync(input.UserId.ToString());

                if (lead.AssignedUser?.Id == user.Id)
                {
                    throw new UserFriendlyException(L("Lead already assigned to this User!"));
                }

                lead.AssignedUser = user;

                await Repository.UpdateAsync(lead);
            }
            catch (Exception e)
            {

                Logger.Error(e.Message);

                if (e.GetType() != typeof(UserFriendlyException))
                {
                    throw new UserFriendlyException(L("User not Found!"));
                }

                throw new UserFriendlyException(L(e.Message));

            }
            
        }

        public override async Task<PagedResultDto<LeadDto>> GetAllAsync(PagedLeadResultRequestDto input)
        {
            //var query = CreateFilteredQuery(input);

            //var queryResult = await query.ToListAsync<Lead>();

            //var items = ObjectMapper.Map<IReadOnlyList<LeadDto>>(queryResult);

            //return await Task.Run(() => new PagedResultDto<LeadDto>(items.Count, items));

            return await base.GetAllAsync(input);

        }

        protected override IQueryable<Lead> CreateFilteredQuery(PagedLeadResultRequestDto input)
        {
            return Repository.GetAllIncluding(l => l.AssignedUser)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), 
                l => l.Name.Contains(input.Keyword)
                     || l.LastName.Contains(input.Keyword)
                     || l.AssignedUser.Name.Contains(input.Keyword)
                     || l.AssignedUser.Surname.Contains(input.Keyword)
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
            var lead = await GetEntityByIdAsync(input.Id);
            var leadEditDto = ObjectMapper.Map<LeadEditDto>(lead);
            var users = await _userStore.GetUsersInRoleAsync("SDR");

            return new GetLeadForEditOutput
            {
                Lead = leadEditDto,
                Users = ObjectMapper.Map<List<UserDto>>(users)
            };
        }
    }
}
