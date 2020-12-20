﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSaver.Contexts;
using SmartSaver.Models;
using SmartSaver.Service;


namespace SmartSaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RESTAuthorize]

    public class UserInformationsController : ControllerBase
    {
        private readonly IUserServices _userService;

        public UserInformationsController(IUserServices userService)
        {
            _userService = userService;
        }



/*        [HttpGet]
        public JsonResult Person(string query)
        {
            var data = PersonRepository.Find(query);

            return Json(data, JsonRequestBehavior.AllowGet);
        }*/

        // GET: api/SavingsManagerInformations/
        [HttpGet("{email}/{password}")]
        public async Task<ActionResult<UserInformation>> GetUser(string email, string password)
        {
            var check = await _userService.GetUser(email, password);
            if (check.Success)
            {
                return Ok(await _userService.GetUser(email, password));
            }
            else
            {
                return BadRequest();
            }
           
        }

        // POST: api/UserInformations
        [HttpPost]
        public async Task<ActionResult<UserInformation>> PostUserInformation(UserInformation userInformation)
        {
            var check = await _userService.AddUser(userInformation);
            if (check.Success)
            {
             return Ok(await _userService.AddUser(userInformation));
            }
            else
            {
                return BadRequest();
            }
        }

    }
    }

