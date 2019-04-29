namespace DeOlho.Domain
{
    public class PoliticoLegislaturaDespesa
    {
        public virtual PoliticoLegislatura PoliticoLegislatura { get; private set; }
        public virtual Despesa Despesa { get; private set; }
        public virtual DespesaBeneficiario Beneficiario { get; private set; }
    }
}