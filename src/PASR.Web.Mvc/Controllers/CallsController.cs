using Abp.Application.Services.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PASR.Controllers;
using PASR.Calls;
using PASR.Users;
using PASR.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASR.Web.Controllers
{
    public class CallsController : PASRControllerBase
    {
        private readonly ICallAppService _callAppService;

        public CallsController(ICallAppService CallAppService)
        {
            _callAppService = CallAppService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
