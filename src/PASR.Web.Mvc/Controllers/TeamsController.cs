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
    public class TeamsController : PASRControllerBase
    {
        private readonly ITeamAppService _teamAppService;
        //private readonly UserStore _userStore;

        public TeamsController(ITeamAppService leadAppService
                              /*UserStore userStore*/)
        {
            _teamAppService = teamAppService;
           // _userStore = userStore;
        }

        public IActionResult Index()
        {
            var model = new TeamListViewModel{};

            return View(model);
        }
    }
}
