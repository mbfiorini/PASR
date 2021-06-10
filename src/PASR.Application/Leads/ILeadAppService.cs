using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PASR.Leads.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Leads
{
    public interface ILeadAppService : IAsyncCrudAppService<LeadDto, int, PagedLeadResultRequestDto, CreateLeadDto, LeadDto>
    {
        Task<ListResultDto<LeadListOutputDto>> GetLeadsByUserAsync(GetAllLeadsByUserInput input);

        Task<GetLeadForEditOutput> GetLeadForEdit(EntityDto input);


    }
}
