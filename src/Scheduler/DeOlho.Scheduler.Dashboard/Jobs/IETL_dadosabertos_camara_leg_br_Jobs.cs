namespace DeOlho.Scheduler.Dashboard.Jobs
{
    public interface IETL_dadosabertos_camara_leg_br_Jobs
    {
        void ExecuteLegislatura();
        void ExecutePartido();
        void ExecuteDeputado();
        void ExecuteDespesa();
        void ExecuteDespesaLastMonth();
    }
}