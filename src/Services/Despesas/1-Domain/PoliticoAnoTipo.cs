using System;

namespace DeOlho.Services.Despesas.Domain
{
    public class PoliticoAnoTipo : IHasValor, IHasCPF, IHasTipoId, IHasAno
    {
        private PoliticoAnoTipo() {}

        public PoliticoAnoTipo(
            long cpf,
            int tipoId,
            int ano,
            decimal valor)
        {
            CPF = cpf;
            TipoId = tipoId;
            Ano = ano;
            Valor = valor;
        }

        public long CPF { get; private set; }
        public int TipoId { get; private set; }
        public int Ano { get; private set; }
        public decimal Valor { get; private set; }
    }
}
