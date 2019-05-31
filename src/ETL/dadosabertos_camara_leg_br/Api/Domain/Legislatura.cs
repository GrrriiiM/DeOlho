using System;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Domain
{
    public class Legislatura : BaseEntity
    {
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}