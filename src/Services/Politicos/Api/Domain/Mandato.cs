using System;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public class Mandato : Entity
    {
        protected Mandato() 
        {

        }

        public Mandato(
            DateTime dataInicio,
            DateTime dataFim,
            MandatoTipo tipo)
            : this()
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
            Tipo = tipo;
        }

        public DateTime DataInicio { get; protected set; }
        public DateTime DataFim { get; protected set; }
        public MandatoTipo Tipo { get; protected set; }
    }
}