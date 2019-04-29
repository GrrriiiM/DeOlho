using System;
using System.Collections.Generic;

namespace DeOlho.Domain
{
    public class PoliticoLegislatura
    {
        public virtual Politico Politico { get; private set; }
        public virtual Partido Partido { get; private set; }
        public virtual Cargo Cargo { get; private set; }
        public virtual ICollection<PoliticoLegislaturaDespesa> Despesas { get; private set; }
        public virtual ICollection<PoliticoLegislaturaSecretario> Secretarios { get; private set; }
        public virtual PoliticoLegislaturaSituacao Situacao { get; private set; }
        public DateTime DataEntrada { get; private set; }
        public DateTime? DataSaida { get; private set; }


    }
}