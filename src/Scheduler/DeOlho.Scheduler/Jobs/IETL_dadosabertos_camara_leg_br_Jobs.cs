namespace DeOlho.Scheduler.Jobs
{
    public interface IETL_dadosabertos_camara_leg_br_Jobs
    {
        void ExecutePartido();
        void ExecuteLegislatura();
        void ExecuteDeputado();
        void ExecuteDespesa();
        void ExecuteDespesaLastMonth();
    }
}