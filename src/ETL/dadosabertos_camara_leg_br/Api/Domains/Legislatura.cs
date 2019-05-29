using System;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Domains
{
    public class Legislatura : Base
    {
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}