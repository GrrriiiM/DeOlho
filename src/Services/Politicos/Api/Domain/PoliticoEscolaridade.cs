using DeOlho.Services.Politicos.Api.Domain.SeedWork;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public class PoliticoEscolaridade : Enumeration
    {
        public PoliticoEscolaridade(int id, string name) : base(id, name) { }

        public readonly static PoliticoEscolaridade Analfabeto = new PoliticoEscolaridade(1, "Analfabeto");
        public readonly static PoliticoEscolaridade LeEscreve = new PoliticoEscolaridade(2, "LeEscreve");
        public readonly static PoliticoEscolaridade FundamentalIncompleto = new PoliticoEscolaridade(3, "FundamentalIncompleto");
        public readonly static PoliticoEscolaridade Fundamental = new PoliticoEscolaridade(4, "Fundamental");
        public readonly static PoliticoEscolaridade MedioIncompleto = new PoliticoEscolaridade(5, "MedioIncompleto");
        public readonly static PoliticoEscolaridade Medio = new PoliticoEscolaridade(6, "Medio");
        public readonly static PoliticoEscolaridade TecnicoIncompleto = new PoliticoEscolaridade(7, "TecnicoIncompleto");
        public readonly static PoliticoEscolaridade Tecnico = new PoliticoEscolaridade(8, "Tecnico");
        public readonly static PoliticoEscolaridade SuperiorIncomplete = new PoliticoEscolaridade(9, "SuperiorIncomplete");
        public readonly static PoliticoEscolaridade Superior = new PoliticoEscolaridade(10, "Superior");
        public readonly static PoliticoEscolaridade PosGraduacaoIncompleto = new PoliticoEscolaridade(11, "PosGraduacaoIncompleto");
        public readonly static PoliticoEscolaridade PosGraduacao = new PoliticoEscolaridade(12, "PosGraduacao");
        public readonly static PoliticoEscolaridade MestradoIncompleto = new PoliticoEscolaridade(13, "MestradoIncompleto");
        public readonly static PoliticoEscolaridade Mestrado = new PoliticoEscolaridade(14, "Mestrado");
        public readonly static PoliticoEscolaridade DoutoradoIncompleto = new PoliticoEscolaridade(15, "DoutoradoIncompleto");
        public readonly static PoliticoEscolaridade Doutorado = new PoliticoEscolaridade(16, "Doutorado");
        public readonly static PoliticoEscolaridade INCORRETO = new PoliticoEscolaridade(99, "INCORRETO");

        public static implicit operator PoliticoEscolaridade(int id)
        {
            return Get<PoliticoEscolaridade>(id);
        }

        public static implicit operator int(PoliticoEscolaridade enumeration)
        {
            return enumeration == null ? 0 : enumeration.Id;
        }
    }
}