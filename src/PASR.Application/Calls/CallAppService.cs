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

namespace PASR.Calls
{
    public class CallAppService : AsyncCrudAppService<Call,CallDto,int,PagedCallResultRequestDto,CreateCallDto,CallDto>
    {
        public IRepository<Lead> _leadRepository;
        private readonly UserStore _userStore;

        public CallAppService(
            IRepository<Call> repository,
            IRepository<Lead> leadRepository,
            UserStore userStore) : base(repository)
        {
            _leadRepository = leadRepository;
            _userStore = userStore;
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

        public override Task<PagedResultDto<CallDto>> GetAllAsync(PagedCallResultRequestDto input)
        {
            return base.GetAllAsync(input);
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
