using System;

namespace DeOlho.Services.Despesas.Domain
{
    public class TotalPolitico : IHasValor, IHasCPF
    {
        private TotalPolitico() {}

        public TotalPolitico(
            long cpf,
            decimal valor)
        {
            CPF = cpf;
            Valor = valor;
        }

        public long CPF { get; private set; }
        public decimal Valor { get; private set; }
    }
}
