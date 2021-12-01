using Abp.Application.Services.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PASR.Authorization.Users;
using PASR.Controllers;
using PASR.Leads;
using PASR.Teams;
using PASR.Teams.Dto;
using PASR.Users;
using PASR.Users.Dto;
using PASR.Web.Models.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASR.Web.Controllers
{
    public class TeamController : PASRControllerBase
    {
        private readonly ITeamAppService _teamAppService;
        private readonly UserStore _userStore;

        public TeamController(ITeamAppService teamAppService,
                              UserStore userStore)
        {
            _teamAppService = teamAppService;
            _userStore = userStore;
        }

        public IActionResult Index()
        {            
            return View();
        }
    }
}
