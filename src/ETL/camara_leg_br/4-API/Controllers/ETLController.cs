using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeOlho.ETL.camara_leg_br.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeOlho.ETL.camara_leg_br.API.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ETLController : ControllerBase
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
        public async Task<ActionResult> NotasFiscais([FromBody]ExecuteNotaFiscalETLCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
