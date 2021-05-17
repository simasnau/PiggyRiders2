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
            try
            {
                var ID = Int32.Parse(_JWTService.GetID(Request.Cookies["token"]));
                var check = await _userService.DeleteUser(ID);
                if (check.Success)
                {
                    return Ok(check);
                }
                else return BadRequest();
            }catch(Exception ex)
            {
                Console.WriteLine("Unable to delete user, exception: " + ex);
                return BadRequest();
            }



        }

        // PUT: api/UserInformations
        [HttpPut]
        public async Task<ActionResult<UserInformation>> PutUserInformation(UserInformation user)
        {
            string username = _JWTService.GetUsername(Request.Cookies["token"]);
            if (username != null && user.Username != null)
            {
                if (_userService.UsernameExists(user.Username) == false)
                {
                    var newUser = _userService.UpdateUsername(username, user.Username);
                    return Ok();
                }
                else return BadRequest();

            }
            else if (username != null && user.Password != null)
            {
                _userService.UpdatePassword(username, user.Password);
                return Ok();
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("email")]
        public async Task<ActionResult> ResetUserEmail([FromBody] String email)
        {
            var response=_userService.ResetEmail(email);   
            if(response.Success){
                return Ok();
            }
            else return BadRequest();
        }
    }
}

