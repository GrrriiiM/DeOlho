using System;

namespace DeOlho.Services.Despesas.Domain
{
    public class TotalTipo : IHasValor, IHasTipoId
    {
        private TotalTipo() {}

        public TotalTipo(
            int tipoId,
            decimal valor)
        {
            TipoId = tipoId;
            Valor = valor;
        }

        public int TipoId { get; private set; }
        public decimal Valor { get; private set; }
    }
}
