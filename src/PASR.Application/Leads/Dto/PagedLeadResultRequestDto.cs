﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Leads.Dto
{
    public class PagedLeadResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
