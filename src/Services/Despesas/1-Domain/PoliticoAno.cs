using System;

namespace DeOlho.Services.Despesas.Domain
{
    public class PoliticoAno
    {
        public PoliticoAno(
            long cpf,
            int ano,
            decimal valor)
        {
            CPF = cpf;
            Ano = ano;
            Valor = valor;
        }

        public long CPF { get; private set; }
        public int Ano { get; private set; }
        public decimal Valor { get; private set; }
    }
}
