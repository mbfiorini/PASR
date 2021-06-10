using System.Threading.Tasks;
using PASR.Configuration.Dto;

namespace PASR.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
