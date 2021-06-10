using System.Threading.Tasks;
using Abp.Application.Services;
using PASR.Authorization.Accounts.Dto;

namespace PASR.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
