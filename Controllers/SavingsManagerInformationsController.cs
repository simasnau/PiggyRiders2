﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSaver.Contexts;
using SmartSaver.Models;
using SmartSaver.Service.SavingService;

namespace SmartSaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingsManagerInformationsController : ControllerBase
    {
        private readonly ISavingService _savingService;

        public SavingsManagerInformationsController(ISavingService savingService)
        {
            _savingService = savingService;
        }

        // GET: api/SavingsManagerInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SavingsManagerInformation>>> Get()
        {
            return Ok(await _savingService.GetAllSavings());
        }

        // GET: api/SavingsManagerInformations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SavingsManagerInformation>> GetSingle(int id)
        {
            return Ok(await _savingService.GetSavingsById(id));
        }

        // PUT: api/SavingsManagerInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSaving(SavingsManagerInformation updatedSaving, int id)
        {
            ServiceResponse<SavingsManagerInformation> response = await _savingService.UpdateSaving(updatedSaving);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/SavingsManagerInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SavingsManagerInformation>> PostSavingsManagerInformation(SavingsManagerInformation newSaving)
        {
            return Ok(await _savingService.AddSaving(newSaving));
        }

        // DELETE: api/SavingsManagerInformations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<SavingsManagerInformation>> response = await _savingService.DeleteSaving(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

       
        
    }
    
}

