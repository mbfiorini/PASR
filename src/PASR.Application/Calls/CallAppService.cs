using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Calls
{
    public class CallAppService : AsyncCrudAppService<Call,EntityDto>
    {
        public CallAppService(IRepository<Call> repository) : base (repository)
        {
        }
    }
}
