namespace DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Requests
{
    public class DespesaRequest : BaseRequest
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}