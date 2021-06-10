using Abp.Application.Services;
using PASR.MultiTenancy.Dto;

namespace PASR.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

