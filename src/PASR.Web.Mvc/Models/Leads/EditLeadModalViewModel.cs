using Abp.AutoMapper;
using PASR.Leads.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASR.Web.Models.Leads
{
    [AutoMapFrom(typeof(GetLeadForEditOutput))]
    public class EditLeadModalViewModel : GetLeadForEditOutput
    {
        
    }
}
