using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Domain;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Infrastructure.Data;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Interfaces;
using DeOlho.ETL.EFCore.Destinations;
using DeOlho.ETL.EFCore.Sources;
using DeOlho.ETL.Sources;
using DeOlho.ETL.Transforms;
using DeOlho.EventBus.Services.Politicos.Messages;
using RawRabbit;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Services
{
    public class PartidoService : IPartidoService
    {
        readonly IETLConfiguration _configuration;
        readonly DeOlhoDbContext _deOlhoDbContext;
        readonly HttpClient _httpClient;
        readonly IBusClient _busClient;
        public PartidoService(
            HttpClient httpClient,
            DeOlhoDbContext deOlhoDbContext,
            IETLConfiguration configuration,
            IBusClient busClient)
        {
            _httpClient =  httpClient;
            _deOlhoDbContext = deOlhoDbContext;
            _configuration = configuration;
            _busClient = busClient;
        }

        public async Task ExecuteETL()
        {
            try
            {
                var load = await new Process()
                    .Extract(() => new HttpJsonSource(this._httpClient, this._configuration.PartidoURL))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.Value.dados))
                    .Extract(_ => new HttpJsonSource(this._httpClient, string.Format(this._configuration.PartidoDetailWithIdArgURL, _.Value.id)))
                    .TransformJsonToDynamic()
                    .Transform(Transform)
                    .Extract(_ => new DbContextSingleOrDefaultSource<Partido>(_deOlhoDbContext, _.Value.Id))
                    .Where(_ => compare(_.Value, (Partido)_.Parent.Value))
                    .Select(_ => (Partido)_.Parent.Value)
                    .ParallelForEach(_ => _busClient.PublishAsync(Message(_)))
                    .ToStepCollection()
                    .Load(() => new DbContextDestination(_deOlhoDbContext));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private bool compare(Partido o, Partido n)
        {
            return o == null
                || o.Sigla != n.Sigla
                || o.Nome != n.Nome
                || o.Data != n.Data
                || o.LegislaturaId != n.LegislaturaId
                || o.Situacao != n.Situacao
                || o.TotalPosse != n.TotalPosse
                || o.TotalMembros != n.TotalMembros
                || o.LiderId != n.LiderId
                || o.UrlFacebook != n.UrlFacebook
                || o.UrlLogo != n.UrlLogo
                || o.UrlWebSite != n.UrlWebSite;
        }

        protected Partido Transform(dynamic _)
        {
            return new Partido {
                Id = (long)_.Value.dados.id,
                Sigla = (string)_.Value.dados.sigla,
                Nome = (string)_.Value.dados.nome,
                Data = _.Value.dados.status.data != null ? new Nullable<DateTime>(DateTime.Parse((string)_.Value.dados.status.data)) : null,
                LegislaturaId = (long)_.Value.dados.status.idLegislatura,
                Situacao = (string)_.Value.dados.status.situacao,
                TotalPosse = (int)_.Value.dados.status.totalPosse,
                TotalMembros = (int)_.Value.dados.status.totalMembros,
                LiderId = (string)_.Value.dados.status.lider.uri,
                UrlFacebook = (string)_.Value.dados.urlFacebook,
                UrlLogo = (string)_.Value.dados.urlLogo,
                UrlWebSite = (string)_.Value.dados.urlWebSite
            };
        }

        protected PartidoMessage Message(Partido _)
        {
            return new PartidoMessage {
                Id = _.Id, 
                Sigla = _.Sigla,
                Nome = _.Nome,
                Data = _.Data,
                LegislaturaId = _.LegislaturaId,
                Situacao = _.Situacao,
                TotalPosse = _.TotalPosse,
                TotalMembros = _.TotalMembros,
                LiderId = _.LiderId,
                UrlFacebook = _.UrlFacebook,
                UrlLogo = _.UrlLogo,
                UrlWebSite = _.UrlWebSite
            };
        }

    }
}