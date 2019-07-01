using System;

namespace DeOlho.Services.Despesas.Domain
{
    public class TotalAnoMes : IHasValor, IHasAno, IHasMes
    {
        private TotalAnoMes() {}

        public TotalAnoMes(
            int mes,
            int ano,
            decimal valor)
        {
            Mes = mes;
            Ano = ano;
            Valor = valor;
        }

        public int Mes { get; private set; }
        public int Ano { get; private set; }
        public decimal Valor { get; private set; }
    }
}
