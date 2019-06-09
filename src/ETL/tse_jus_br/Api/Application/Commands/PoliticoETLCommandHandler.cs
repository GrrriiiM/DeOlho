using System.Linq;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.ETL.Sources;
using DeOlho.ETL.EFCore.Destinations;
using DeOlho.ETL.EFCore.Sources;
using DeOlho.ETL.Transforms;
using DeOlho.ETL.tse_jus_br.Api.Domain;
using MediatR;
using System.Collections.Generic;
using DeOlho.ETL.tse_jus_br.Api.Infrastructure.Data;
using DeOlho.ETL.tse_jus_br.Api;
using System;

namespace DeOlho.ETL.tse_jus_br.Api.Application.Commands
{
    public class PoliticoETLCommandHandler : IRequestHandler<PoliticoETLCommand>
    {
        
        readonly PoliticoFactory _politicoFactory;
        readonly IETLConfiguration _configuration;
        readonly IPoliticoRepository _politicoRepository;
        readonly DeOlhoDbContext _deOlhoDbContext;
        readonly IHttpClientFactory _httpClientFactory;

        public PoliticoETLCommandHandler(
            PoliticoFactory politicoFactory,
            IETLConfiguration configuration,
            IPoliticoRepository politicoRepository,
            DeOlhoDbContext deOlhoDbContext,
            IHttpClientFactory httpClientFactory
        )
        {
            _politicoFactory = politicoFactory;
            _configuration = configuration;
            _politicoRepository = politicoRepository;
            _deOlhoDbContext = deOlhoDbContext;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Unit> Handle(PoliticoETLCommand request, CancellationToken cancellationToken)
        {
            var cultureInfo = new CultureInfo("pt-BR");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            var year = request.Year ?? DateTime.Now.Year;

            var load = await new Process()
                .Extract(() => new HttpStreamSource(_httpClientFactory.CreateClient(), string.Format(_configuration.PoliticosUrl, year)))
                .TransformDescompressStream()
                .TransformToList(_ => _.Value)
                .Where(_ => _.Value.Name.ToUpper() == $"CONSULTA_CAND_{year}_BRASIL.CSV")
                .Select(_ => _.Value.Stream)
                .TransformCsvToDynamic(";")
                .TransformToList(_ => new List<dynamic>(_.Value))
                .Select(_ => (Politico)_politicoFactory.Transform(_.Value))
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Where(_ => _.Value.CD_CARGO == 6 && (_.Value.CD_SIT_TOT_TURNO == 2 || _.Value.CD_SIT_TOT_TURNO == 3))
                .Select(_ => _politicoRepository.FindByCPFAsync(_.Value.NR_CPF_CANDIDATO).Result)
                .Where(_ => !((Politico)_.Parent.Value).IsEqual(_.Value))
                .Select(_ => (Politico)_.Parent.Value)
                .Load(() => new DbContextDestination(_deOlhoDbContext));

            await _deOlhoDbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}