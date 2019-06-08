using System;
using System.Data;
using System.Threading.Tasks;
using DeOlho.ETL.tse_jus_br.Api.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace DeOlho.ETL.tse_jus_br.Api.Controllers
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
        public async Task<ActionResult> Politicos([FromBody]PoliticoETLCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}