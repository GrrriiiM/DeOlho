using System;
using System.Collections.Generic;
using System.Linq;
using DeOlho.Services.Politicos.Api.Domain.SeedWork;

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
        public int EscolaridadeId { get; set; } 
        public PoliticoEscolaridade Escolaridade { get => EscolaridadeId; }
        public int SituacaoId { get; set;}
        public PoliticoSituacao Situacao { get => SituacaoId; }
        public int SexoId { get; set; }
        public PoliticoSexo Sexo { get => SexoId; }
        public int MandatoTipoId { get; set; }
        public MandatoTipo MandatoTipo { get => MandatoTipoId; }
        public DateTime? MandatoInicio { get; set; }
        public DateTime? MandatoFim { get; set; }
        public int MandatoSituacaoId { get; set; }
        public MandatoSituacao MandatoSituacao { get => MandatoSituacaoId; }

        private List<PoliticoContato> _contatos = new List<PoliticoContato>();
        public IReadOnlyCollection<PoliticoContato> Contatos { get => _contatos.AsReadOnly(); protected set => _contatos = value.ToList(); }

        public PoliticoContato AddContato(ContatoTipo tipo, string contato)
        {
            var c = new PoliticoContato(this, tipo, contato);
            _contatos.Add(c);
            return c;
        }

    }
}