using System;

namespace DeOlho.Services.Despesas.Domain
{
    public class PoliticoMes : IHasValor, IHasCPF, IHasAno, IHasMes
    {
        private PoliticoMes() {}

        public PoliticoMes(
            long cpf,
            int mes,
            int ano,
            decimal valor)
        {
            CPF = cpf;
            Mes = mes;
            Ano = ano;
            Valor = valor;
        }

        public long CPF { get; private set; }
        public int Mes { get; private set; }
        public int Ano { get; private set; }
        public decimal Valor { get; private set; }
    }
}
