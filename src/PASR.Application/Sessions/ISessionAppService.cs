using System.Threading.Tasks;
using Abp.Application.Services;
using PASR.Sessions.Dto;

namespace PASR.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
