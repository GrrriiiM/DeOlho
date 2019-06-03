using System;
using System.Collections.Generic;

namespace DeOlho.EventBus.Services.Politicos.Messages
{
    public class PoliticoChangeMessage : BaseMessage
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime Nascimento { get; set; }
        public DateTime? Falecimento { get; set; }
        public string NascimentoUF { get; set; }
        public string NascimentoMunicipio { get; set; }
        /// <summary> 
        ///Nenhum = 0,
        ///FundamentalIncompleto = 10,
        ///Fundamental = 15,
        ///MedioIncompleto = 20,
        ///Medio = 25,
        ///Tecnico = 30,
        ///Tecnico = 35,
        ///SuperiorIncomplete = 40,
        ///Superior = 45,
        ///PosGraduacaoIncompleto = 50,
        ///PosGraduacao = 55,
        ///MestradoIncompleto = 60,
        ///Mestrado = 65,
        ///DoutoradoIncompleto = 705,
        ///Doutorado = 75,
        ///INCORRETO = 99
        /// </summary>
        public int Escolaridade { get; set; }
        /// <summary>
        /// Nenhum = 0,
        /// Aposentado = 1,
        /// Preso = 2,
        /// Falecido = 3,
        /// INCORRETO = 99
        /// </summary>
        public int Situacao { get; set; }
        /// <summary>
        /// Nenhum = 0,
        /// Masculino = 1,
        /// Feminino = 2,
        /// Outro = 50,
        /// INCORRETO = 99,
        /// </summary>
        public int Sexo { get; set; }
        public string Partido { get; set; }
        public int MandatoTipo { get; set; }
        public DateTime? MandatoInicio { get; set; }
        public DateTime? MandatoFim { get; set; }
        /// <summary>
        /// Nenhum = 0,
        /// Exercicio = 1,
        /// Afastado = 2,
        /// Convocado = 3,
        /// FimMandato = 4,
        /// Licenca = 5,
        /// Suplencia = 6,
        /// Suspenso = 7,
        /// Vacancia = 8
        /// </summary>
        /// <value></value>
        public int MandatoSituacao { get; set; }

        public List<Contato> Contatos { get; set; } = new List<Contato>();
        public class Contato
        {
            public int Tipo { get; set; }
            public string Valor { get; set; }
        }
    }
}

