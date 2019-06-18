using System;
using System.Data;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Interfaces;
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
        readonly IMediator _mediator;
        public ETLController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<ActionResult> DeputadosFederais([FromBody]ExecuteDeputadoFederalETLCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost()]
        public async Task<ActionResult> Partidos([FromServices]IPartidoService partidoService)
        {
            await partidoService.ExecuteETL();
            return Ok();
        }

        [HttpPost()]
        public async Task<ActionResult> Politicos([FromServices]IPoliticoService politicoService)
        {
            await politicoService.ExecuteETL();
            return Ok();
        }

        [HttpPost()]
        public async Task<ActionResult> Despesas([FromServices]IDespesaService despesaService, int? year = null, int? month = null)
        {
            year = year ?? DateTime.Now.Year;
            month = month ?? DateTime.Now.Month;
            await despesaService.ExecuteETL(year.Value, month.Value);
            return Ok();
        }
    }
}