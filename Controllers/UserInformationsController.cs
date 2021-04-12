using System;
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

    public class UserInformationsController : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly IJWTService _JWTService;


        public UserInformationsController(IUserServices userService, IJWTService jwtService)
        {
            _userService = userService;
            _JWTService = jwtService;

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

        // GET: api/UserInformation
        //Barto
        public async Task<ActionResult<IEnumerable<UserInformation>>> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        // DELETE: api/UserInformations
        [HttpDelete]
        public async Task<ActionResult<UserInformation>> DeleteUser()
        {
            var ID =Int32.Parse(_JWTService.GetID());

            var check = await _userService.DeleteUser(ID);
            if (check.Success)
            {
                return Ok(check);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}

