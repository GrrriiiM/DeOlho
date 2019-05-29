using System;
using System.Data;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Services;
// using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Requests;
// using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Responses;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class ETLController : Controller
    {
        readonly IETLService _etlService;

        // readonly IBusClient _busClient;

        public ETLController(
            IETLService etlService
            // ,
            // IBusClient busClient
            )
        {
            _etlService = etlService;
            // _busClient = busClient;
        }

        [HttpPost()]
        public async Task<ActionResult> Legislaturas()
        {
            await _etlService.ExecuteLegislatura();
            
            return Ok();
        }


        



        [HttpPost()]
        public async Task<ActionResult> Partidos()
        {
            await _etlService.ExecutePartido();
            return Ok();
        }

        [HttpPost()]
        public async Task<ActionResult> Deputados()
        {
            await _etlService.ExecuteDeputado();
            return Ok();
        }

        [HttpPost()]
        public async Task<ActionResult> Despesas(int? year = null, int? month = null)
        {
            year = year ?? DateTime.Now.Year;
            month = month ?? DateTime.Now.Month;
            await _etlService.ExecuteDespesa(year.Value, month.Value);
            return Ok();
        }
        

        

    }
}