using DeOlho.Services.Politicos.Api.Domain.SeedWork;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public class ContatoTipo : Enumeration
    {
        public ContatoTipo(int id, string name) : base(id, name) { }

        public readonly static ContatoTipo Email = new ContatoTipo(1, "Email");
        public readonly static ContatoTipo Facebook = new ContatoTipo(2, "Facebook");
        public readonly static ContatoTipo Twitter = new ContatoTipo(3, "Twitter");
        public readonly static ContatoTipo Site = new ContatoTipo(4, "Site");
        public readonly static ContatoTipo Telefone = new ContatoTipo(5, "Telefone");

        public static implicit operator ContatoTipo(int id)
        {
            return Get<ContatoTipo>(id);
        }

        public static implicit operator int(ContatoTipo enumeration)
        {
            return enumeration == null ? 0 : enumeration.Id;
        }
    }
}