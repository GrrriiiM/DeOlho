using System;

namespace DeOlho.Services.Despesas.Domain
{
    public class PoliticoAnoMesTipo : IHasValor, IHasCPF, IHasAno, IHasMes, IHasTipoId
    {
        private PoliticoAnoMesTipo() {}

        public PoliticoAnoMesTipo(
            long cpf,
            int tipoId,
            int mes,
            int ano,
            decimal valor)
        {
            CPF = cpf;
            TipoId = tipoId;
            Mes = mes;
            Ano = ano;
            Valor = valor;
        }

        public long CPF { get; private set; }
        public int TipoId { get; private set; }
        public int Mes { get; private set; }
        public int Ano { get; private set; }
        public decimal Valor { get; private set; }
    }
}
