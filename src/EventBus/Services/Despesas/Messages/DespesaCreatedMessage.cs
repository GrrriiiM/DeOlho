namespace DeOlho.EventBus.Services.Despesas.Messages
{
    public class DespesaCreatedMessage : Message
    {
        public long CPF { get; set; }
        public int TipoId { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public decimal Valor { get; set; }
    }
}