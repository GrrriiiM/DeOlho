using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeOlho.Services.Despesas.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeOlho.Services.Despesas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        readonly IMediator _mediator;

        public DespesasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/values
        [HttpPost]
        public async void CreateDespesas([FromBody] CreateDespesasCommand command)
        {
            await _mediator.Send(command);
        }
    }
}
