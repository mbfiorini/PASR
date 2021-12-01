using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using PASR.Authorization.Users;
using PASR.Calls.Dto;
using PASR.Leads;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace PASR.Calls
{
    public class CallAppService : AsyncCrudAppService<Call,CallDto,int,PagedCallResultRequestDto,CreateCallDto,CallDto>
    {
        public IRepository<Lead> _leadRepository;
        public IRepository<Call> _callRepository;
        private readonly UserStore _userStore;

        public CallAppService(
            IRepository<Call> repository,
            IRepository<Lead> leadRepository,
            UserStore userStore, 
            IRepository<Call> callRepository) : base(repository)
        {
            _leadRepository = leadRepository;
            _userStore = userStore;
            _callRepository = callRepository;
        }

        public async override Task<CallDto> CreateAsync(CreateCallDto createCallDto)
        {
            try
            {
                createCallDto.Lead = await _leadRepository.FirstOrDefaultAsync(createCallDto.LeadId);
                createCallDto.User = await _userStore.FindByIdAsync(AbpSession.UserId.ToString());                 
            }
            catch (System.Exception)
            {
                throw new UserFriendlyException("User or Lead not Found!");
            }

            if (createCallDto.Significant)
            {
                createCallDto.callResult = createCallDto.Scheduled ? CallResult.ScheduledMeeting : CallResult.Significant;
            }else{
                createCallDto.callResult = CallResult.NotSignificant;
            }

            Call call = ObjectMapper.Map<Call>(createCallDto);

            call = await Repository.InsertAsync(call);

            return MapToEntityDto(call);
        }

        public override Task DeleteAsync(EntityDto<int> input)
        {
            return base.DeleteAsync(input);
        }

        public async override Task<PagedResultDto<CallDto>> GetAllAsync(PagedCallResultRequestDto input)
        {
            if(input.Id == 0) throw new UserFriendlyException("Invalid 'Id' property passed as argument");
            
            IQueryable query = _callRepository
                                   .GetAllIncluding(c => c.Lead, c => c.User)
                                   .Where(c => c.Lead.Id == input.Id)
                                   .Skip(input.SkipCount)
                                   .Take(input.MaxResultCount);
            
            string exp =  query.ToQueryString();

            //I used task run, because there is no AsyncMethod for GetAllIncluding
            List<Call> Calls = await Task.Run(() => _callRepository
                                                        .GetAllIncluding(c => c.Lead, c => c.User)
                                                        .Where(c => c.Lead.Id == input.Id)
                                                        .Skip(input.SkipCount)
                                                        .Take(input.MaxResultCount).ToList());

            return new PagedResultDto<CallDto>(Calls.Count,ObjectMapper.Map<List<CallDto>>(Calls));
            
        }

        public override Task<CallDto> GetAsync(EntityDto<int> input)
        {
            return base.GetAsync(input);
        }

        public override Task<CallDto> UpdateAsync(CallDto input)
        {
            return base.UpdateAsync(input);
        }

        protected override IQueryable<Call> CreateFilteredQuery(PagedCallResultRequestDto input)
        {
            return base.CreateFilteredQuery(input);
        }
    }
}