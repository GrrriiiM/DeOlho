using DeOlho.Services.Politicos.Api.Domain.SeedWork;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public class MandatoSituacao : Enumeration
    {
        public MandatoSituacao(int id, string name) : base(id, name) { }

        public readonly static MandatoSituacao Nenhum = new MandatoSituacao(1, "Nenhum");
        public readonly static MandatoSituacao Exercicio = new MandatoSituacao(2, "Exercicio");
        public readonly static MandatoSituacao Afastado = new MandatoSituacao(3, "Afastado");
        public readonly static MandatoSituacao Convocado = new MandatoSituacao(4, "Convocado");
        public readonly static MandatoSituacao FimMandato = new MandatoSituacao(5, "FimMandato");
        public readonly static MandatoSituacao Licenca = new MandatoSituacao(6, "Licenca");
        public readonly static MandatoSituacao Suplencia = new MandatoSituacao(7, "Suplencia");
        public readonly static MandatoSituacao Suspenso = new MandatoSituacao(8, "Suspenso");
        public readonly static MandatoSituacao Vacancia = new MandatoSituacao(9, "Vacancia");

        public static implicit operator MandatoSituacao(int id)
        {
            return Get<MandatoSituacao>(id);
        }

        public static implicit operator int(MandatoSituacao enumeration)
        {
            return enumeration == null ? 0 : enumeration.Id;
        }
        
    }
}