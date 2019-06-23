using System.Threading;
using System.Threading.Tasks;
using DeOlho.Services.Despesas.Infrastucture.Data;
using MediatR;

namespace DeOlho.Services.Despesas.Application.Commands
{
    public class CreatePoliticoMesTipoCommand : IRequest
    {
        public long CPF { get; set; }
        public int TipoId { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public decimal Valor { get; set; }
    }

    public class CreatePoliticoMesTipoCommandHandler : IRequestHandler<CreatePoliticoMesTipoCommand>
    {
        readonly DeOlhoDbContext _deOlhoDbContext;

        public CreatePoliticoMesTipoCommandHandler(DeOlhoDbContext deOlhoDbContext)
        {
            _deOlhoDbContext = deOlhoDbContext;
        } 

        public Task<Unit> Handle(CreatePoliticoMesTipoCommand request, CancellationToken cancellationToken)
        {
            
            return null;
        }
    }
}