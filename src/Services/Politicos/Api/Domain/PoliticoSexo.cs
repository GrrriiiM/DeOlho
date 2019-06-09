using DeOlho.Services.Politicos.Api.Domain.SeedWork;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public class PoliticoSexo : Enumeration
    {
        public PoliticoSexo(int id, string name) : base(id, name) { }

        public readonly static PoliticoSexo Masculino = new PoliticoSexo(1, "Masculino");
        public readonly static PoliticoSexo Feminino = new PoliticoSexo(2, "Feminino");
        public readonly static PoliticoSexo Outro = new PoliticoSexo(3, "Outro");
        public readonly static PoliticoSexo INCORRETO = new PoliticoSexo(99, "INCORRETO");

        public static implicit operator PoliticoSexo(int id)
        {
            return Get<PoliticoSexo>(id);
        }

        public static implicit operator int(PoliticoSexo enumeration)
        {
            return enumeration == null ? 0 : enumeration.Id;
        }
    }
}