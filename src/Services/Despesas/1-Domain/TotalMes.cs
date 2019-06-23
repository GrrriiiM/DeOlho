using System;

namespace DeOlho.Services.Despesas.Domain
{
    public class TotalMes
    {
        public TotalMes(
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
