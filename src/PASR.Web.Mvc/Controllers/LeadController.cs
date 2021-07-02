using Abp.Application.Services.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PASR.Controllers;
using PASR.Leads;
using PASR.Users;
using PASR.Users.Dto;
using PASR.Web.Models.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASR.Web.Controllers
{
    public class LeadController : PASRControllerBase
    {
        private readonly ILeadAppService _leadAppService;
        //private readonly UserStore _userStore;

        public LeadController(ILeadAppService leadAppService
                              /*UserStore userStore*/)
        {
            _leadAppService = leadAppService;
           // _userStore = userStore;
        }

        public IActionResult Index()
        {
            var model = new LeadListViewModel {};

            return View(model);
        }

        public async Task<ActionResult> EditModal(int leadId)
        {
            var output = await _leadAppService.GetLeadForEdit(new EntityDto(leadId));
            var model = ObjectMapper.Map<EditLeadModalViewModel>(output);

            return PartialView("_EditModal", model);
        }
    }
}
