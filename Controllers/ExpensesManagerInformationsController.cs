﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSaver.Contexts;
using SmartSaver.Models;
using SmartSaver.Services;

namespace SmartSaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesManagerInformationsController : ControllerBase
    {
        private readonly ILimitsService service;
        public ExpensesManagerInformationsController(ILimitsService service)
        {
            this.service = service;
        }

        // GET: api/ExpensesManagerInformations
        [HttpGet]
        public async Task<List<ExpensesManagerInformation>> GetEMInfo()
        {
            if (Request.Cookies.Keys.Contains("token"))
            {
                this.service.cookie = Request.Cookies["token"];
            }
            else this.service.cookie = null;
            var all = await service.GetAll();
            return all;
        }

        // GET: api/ExpensesManagerInformations/5
        [HttpGet("{id}")]
        public async Task<ExpensesManagerInformation> GetExpensesManagerInformation(int id)
        {
            if (Request.Cookies.Keys.Contains("token"))
            {
                this.service.cookie = Request.Cookies["token"];
            }
            else this.service.cookie = null;
            var byId = await service.GetById(id);
            return byId;

        }

        // PUT: api/ExpensesManagerInformations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpensesManagerInformation(int id, ExpensesManagerInformation expensesManagerInformation)
        {
            if (Request.Cookies.Keys.Contains("token"))
            {
                this.service.cookie = Request.Cookies["token"];
            }
            else this.service.cookie = null;
            if (id != expensesManagerInformation.ID)
                return BadRequest();
            await service.Edit(expensesManagerInformation, id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutFromBudgetManager(ExpensesManagerInformation expensesManagerInformation)
        {
            if (Request.Cookies.Keys.Contains("token"))
            {
                this.service.cookie = Request.Cookies["token"];
            }
            else this.service.cookie = null;
            await service.EditFromBudgetManager(expensesManagerInformation);
            return NoContent();
        }

        // POST: api/ExpensesManagerInformations
        [HttpPost]
        public async Task<ActionResult<ExpensesManagerInformation>> PostExpensesManagerInformation(ExpensesManagerInformation expensesManagerInformation)
        {
            if (Request.Cookies.Keys.Contains("token"))
            {
                this.service.cookie = Request.Cookies["token"];
            }
            else this.service.cookie = null;
            await service.Add(expensesManagerInformation);
            return NoContent();
        }



        // DELETE: api/ExpensesManagerInformations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpensesManagerInformation(int id)
        {
            if (Request.Cookies.Keys.Contains("token"))
            {
                this.service.cookie = Request.Cookies["token"];
            }
            else this.service.cookie = null;
            await service.Delete(id);
            return NoContent();
        }
    }


}
