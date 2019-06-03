using System;
using System.Collections;
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
    public class DespesaService : IDespesaService
    {
        readonly IETLConfiguration _configuration;
        readonly DeOlhoDbContext _deOlhoDbContext;
        readonly HttpClient _httpClient;
        readonly IBusClient _busClient;
        public DespesaService(
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

        public async Task ExecuteETL(int year, int month)
        {
            try
            {
                var load = await new Process()
                    .Extract(() => new HttpJsonSource(this._httpClient, this._configuration.DeputadoURL))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.Value.dados))
                    .Extract(_ => new HttpJsonSource(this._httpClient, string.Format(this._configuration.DespesaDetailWithIdMonthYeahArgURL, _.Value.id, year, month)))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.Value.dados))
                    .Transform(Transform)
                    .Extract(_ => new DbContextSingleOrDefaultSource<Despesa>(_deOlhoDbContext, _.Value.Id))
                    .Where(_ => compare(_.Value, (Despesa)_.Parent.Value))
                    .Select(_ => (Despesa)_.Parent.Value)
                    .ParallelForEach(_ => _busClient.PublishAsync(Message(_)))
                    .ToStepCollection()
                    .Load(() => new DbContextDestination(_deOlhoDbContext));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private bool compare(Despesa o, Despesa n)
        {
            return o == null
                || o.DeputadoId != n.DeputadoId
                || o.Ano != n.Ano
                || o.CnpjCpfFornecedor != n.CnpjCpfFornecedor
                || o.CodDocumento != n.CodDocumento
                || o.CodLote != n.CodLote
                || o.CodTipoDocumento != n.CodTipoDocumento
                || o.DataDocumento != n.DataDocumento
                || o.Mes != n.Mes
                || o.NomeFornecedor != n.NomeFornecedor
                || o.NumDocumento != n.NumDocumento
                || o.NumRessarcimento != n.NumRessarcimento
                || o.Parcela != n.Parcela
                || o.TipoDespesa != n.TipoDespesa
                || o.TipoDocumento != n.TipoDocumento
                || o.URLDocumento != n.URLDocumento
                || o.ValorDocumento != n.ValorDocumento
                || o.ValorGlosa != n.ValorGlosa
                || o.ValorLiquido != n.ValorLiquido;
        }

        protected Despesa Transform(dynamic _)
        {
            return new Despesa {
                DeputadoId = (long)((dynamic)_.Parent.Parent.Parent.Value).id,
                Ano = (int)_.Value.ano,
                CnpjCpfFornecedor = (string)_.Value.cnpjCpfFornecedor,
                CodDocumento = (long)_.Value.codDocumento,
                CodLote = (long)_.Value.codLote,
                CodTipoDocumento = (int?)_.Value.codTipoDocumento,
                DataDocumento = (DateTime?)_.Value.dataDocumento,
                Mes = (int)_.Value.mes,
                NomeFornecedor = (string)_.Value.nomeFornecedor,
                NumDocumento = (string)_.Value.numDocumento,
                NumRessarcimento = (string)_.Value.numRessarcimento,
                Parcela = (int)_.Value.parcela,
                TipoDespesa = (string)_.Value.tipoDespesa,
                TipoDocumento = (string)_.Value.tipoDocumento,
                URLDocumento = (string)_.Value.urlDocumento,
                ValorDocumento = (decimal)_.Value.valorDocumento,
                ValorGlosa = (decimal)_.Value.valorGlosa,
                ValorLiquido = (decimal)_.Value.valorLiquido
            };
        }

        protected DespesaMessage Message(Despesa _)
        {
            return new DespesaMessage {
                Id = _.Id, 
                DeputadoId = _.Ano,
                Ano = _.Ano,
                CnpjCpfFornecedor = _.CnpjCpfFornecedor,
                CodDocumento = _.CodDocumento,
                CodLote = _.CodLote,
                CodTipoDocumento = _.CodTipoDocumento,
                DataDocumento = _.DataDocumento,
                Mes = _.Mes,
                NomeFornecedor = _.NomeFornecedor,
                NumDocumento = _.NumDocumento,
                NumRessarcimento = _.NumRessarcimento,
                Parcela = _.Parcela,
                TipoDespesa = _.TipoDespesa,
                TipoDocumento = _.TipoDocumento,
                URLDocumento = _.URLDocumento,
                ValorDocumento = _.ValorDocumento,
                ValorGlosa = _.ValorGlosa,
                ValorLiquido = _.ValorLiquido
            };
        }

    }
}