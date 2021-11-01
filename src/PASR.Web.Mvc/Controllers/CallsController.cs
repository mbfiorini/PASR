using Abp.Application.Services.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PASR.Controllers;
using PASR.Callss;
using PASR.Users;
using PASR.Users.Dto;
using PASR.Web.Models.Callss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASR.Web.Controllers
{
    public class CallsController : PASRControllerBase
    {
        private readonly ICallAppService _leadAppService;
        //private readonly UserStore _userStore;

        public CallsController(ICallAppService leadAppService
                              /*UserStore userStore*/)
        {
            _leadAppService = leadAppService;
           // _userStore = userStore;
        }

        public IActionResult Index()
        {
            var model = new CallListViewModel {};

            return View(model);
        }
    }
}
