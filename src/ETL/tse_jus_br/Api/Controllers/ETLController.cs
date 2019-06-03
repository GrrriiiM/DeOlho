using System;
using System.Data;
using System.Threading.Tasks;
using DeOlho.ETL.tse_jus_br.Api.Services;
// using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Requests;
// using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Responses;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace DeOlho.ETL.tse_jus_br.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class ETLController : Controller
    {

        [HttpPost()]
        public async Task<ActionResult> Politicos([FromServices]IPoliticoService legislaturaService, [FromQuery]int? year = null)
        {
            year = year ?? DateTime.Now.Year;
            await legislaturaService.ExecuteETL(year.Value);
            return Ok();
        }
    }
}