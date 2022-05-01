using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Teltonika.DataModels;
using Teltonika.DBContext;
using Teltonika.Hubs;
using Teltonika.Interfaces;
using Teltonika.Services;

namespace Teltonika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Covid19CasesController : ControllerBase
    {
        private readonly ICovid19CaseService _covid19CaseService;
        private readonly IHubContext<ChartHub> _hubContext;

        public Covid19CasesController(ICovid19CaseService covid19CaseService, IHubContext<ChartHub> chartHub)
        {
            _covid19CaseService = covid19CaseService;
            _hubContext = chartHub;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _covid19CaseService.GetCovid19Case());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Covid19Case>> Get(int id)
        {
            var item = await _covid19CaseService.GetCovid19CaseById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Covid19Case>> Create(Covid19Case covidCase)
        {
            if (covidCase == null)
            {
                return BadRequest();
            }
            await _covid19CaseService.AddCovid19Case(covidCase);
            await _hubContext
                    .Clients
                    .All
                    .SendAsync("statistics-changed", "");
            return Ok(covidCase);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Covid19Case>> Update(int id, Covid19Case covidCase)
        {
            if (covidCase == null)
            {
                return BadRequest();
            }

            var ccToUpdate = await _covid19CaseService.GetCovid19CaseById(id);

            if (ccToUpdate == null)
            {
                return NotFound();
            }

            await _covid19CaseService.UpdateCovid19Case(covidCase);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Covid19Case>> Delete(int id)
        {
            await _covid19CaseService.DeleteCovid19Case(id);
            return NoContent();
        }

        //Chart data API
        [HttpGet]
        [Route("AgeBracket/")]
        public async Task<IActionResult> GetAgeBracketData()
        {
            return Ok(await _covid19CaseService.GetAgeBracketData());
        }

        [HttpGet]
        [Route("Gender/")]
        public async Task<IActionResult> GetGenderData()
        {
            return Ok(await _covid19CaseService.GetGenderData());
        }

        [HttpGet]
        [Route("Municipality/")]
        public async Task<IActionResult> GetMunicipalityData()
        {
            return Ok(await _covid19CaseService.GetMunicipalityData());
        }

        [HttpGet]
        [Route("ConfirmationDate/")]
        public async Task<IActionResult> GetConfirmationDateData()
        {
            return Ok(await _covid19CaseService.GetConfirmationDateData());
        }
    }
}
