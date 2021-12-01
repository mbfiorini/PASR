using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Calls.Dto
{
    public class PagedCallResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        
        public int Id { get; set; }

    }
}
