using System;
using System.Collections.Generic;

namespace DeOlho.Domain
{
    public class Politico
    {

        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public virtual ICollection<Legislatura> Legislaturas { get; private set; }

    }
}