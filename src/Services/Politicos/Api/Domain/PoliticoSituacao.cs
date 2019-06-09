using DeOlho.Services.Politicos.Api.Domain.SeedWork;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public class PoliticoSituacao : Enumeration
    {
        public PoliticoSituacao(int id, string name) : base(id, name) { }

        public readonly static PoliticoSituacao Ativo = new PoliticoSituacao(1, "Ativo");
        public readonly static PoliticoSituacao Aposentado = new PoliticoSituacao(2, "Aposentado");
        public readonly static PoliticoSituacao Preso = new PoliticoSituacao(3, "Preso");
        public readonly static PoliticoSituacao Falecido = new PoliticoSituacao(4, "Falecido");
        public readonly static PoliticoSituacao INCORRETO = new PoliticoSituacao(5, "INCORRETO");

        public static implicit operator PoliticoSituacao(int id)
        {
            return Get<PoliticoSituacao>(id);
        }

        public static implicit operator int(PoliticoSituacao enumeration)
        {
            return enumeration == null ? 0 : enumeration.Id;
        }
    }
}