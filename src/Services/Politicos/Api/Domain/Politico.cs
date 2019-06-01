using System;
using System.Collections.Generic;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public class Politico : Entity
    {
        protected Politico()
        {
            
        }

        public Politico(
            string cpf,
            string nome,
            string apelido,
            DateTime nascimento,
            string nascimentoUF,
            string nacimentoMunicipio)
            : this()
        {
            CPF = cpf;
            Nome = nome;
            Apelido = apelido;
            Nascimento = nascimento;
            NascimentoUF = nascimentoUF;
            NascimentoMunicipio = NascimentoMunicipio;
        }

        public string CPF { get; protected set; }
        public string Nome { get; protected set; }
        public string Apelido { get; protected set; }
        public DateTime Nascimento { get; protected set; }
        public DateTime? Falecimento { get; set; }
        public string NascimentoUF { get; protected set; }
        public string NascimentoMunicipio { get; protected set; }
        public PoliticoEscolaridade Escolaridade { get; set; }
        public PoliticoSituacao Situacao { get; set; }
        public PoliticoSexo Sexo { get; set; }

        private List<PoliticoContato> _contatos = new List<PoliticoContato>();
        public IReadOnlyCollection<PoliticoContato> Contatos { get => _contatos; protected set => _contatos = new List<PoliticoContato>(value); }

        public PoliticoContato AddContato(ContatoTipo tipo, string contato)
        {
            var c = new PoliticoContato(this, tipo, contato);
            _contatos.Add(c);
            return c;
        }

    }
}