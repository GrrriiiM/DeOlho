namespace DeOlho.Services.Despesas.Domain
{
    public class Tipo
    {

        public Tipo(string descricao)
        {
            Descricao = descricao;
        }

        public long Id { get; private set; }
        public string Descricao { get; private set; }
    }
}