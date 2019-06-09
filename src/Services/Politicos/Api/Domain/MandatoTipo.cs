using DeOlho.Services.Politicos.Api.Domain.SeedWork;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public class MandatoTipo : Enumeration
    {
        public MandatoTipo(int id, string name) : base(id, name) { }

        public readonly static MandatoTipo Nenhum = new MandatoTipo(1, "Email");
        public readonly static MandatoTipo DeputadoFederal = new MandatoTipo(2, "DeputadoFederal");

        public static implicit operator MandatoTipo(int id)
        {
            return Get<MandatoTipo>(id);
        }

        public static implicit operator int(MandatoTipo enumeration)
        {
            return enumeration == null ? 0 : enumeration.Id;
        }
    }
}