using System;
using System.Collections.Generic;

namespace DeOlho.EventBus.Services.Politicos.Messages
{
    public class PoliticoChangeMessage : BaseMessage
    {
        public string CPF { get; protected set; }
        public string Nome { get; protected set; }
        public string Apelido { get; protected set; }
        public DateTime Nascimento { get; protected set; }
        public DateTime? Falecimento { get; set; }
        public string NascimentoUF { get; protected set; }
        public string NascimentoMunicipio { get; protected set; }
        public int Escolaridade { get; set; }
        public int Situacao { get; set; }
        public int Sexo { get; set; }

        public List<Contato> Contatos { get; set; } = new List<Contato>();
        public class Contato
        {
            public int Tipo { get; set; }
            public string Valor { get; set; }
        }
    }
}