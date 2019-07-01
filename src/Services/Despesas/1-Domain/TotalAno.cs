using System;

namespace DeOlho.Services.Despesas.Domain
{
    public class TotalAno : IHasValor, IHasAno
    {
        private TotalAno() {}

        public TotalAno(
            int ano,
            decimal valor)
        {
            Ano = ano;
            Valor = valor;
        }

        public int Ano { get; private set; }
        public decimal Valor { get; private set; }
    }
}
