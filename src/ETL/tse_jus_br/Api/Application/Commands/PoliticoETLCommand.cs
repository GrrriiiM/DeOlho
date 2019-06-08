using MediatR;

namespace DeOlho.ETL.tse_jus_br.Api.Application.Commands
{
    public class PoliticoETLCommand : IRequest
    {
        public int Year { get; set; }
    }
}