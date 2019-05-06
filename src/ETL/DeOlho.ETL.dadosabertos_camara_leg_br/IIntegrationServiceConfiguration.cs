namespace DeOlho.ETL.dadosabertos_camara_leg_br
{
    public interface IIntegrationServiceConfiguration
    {
        string PartidoTableName { get; }
        string PartidoURL { get; }
        string PartidoDetailWithIdArgURL { get; }
        

        string LegislaturaTableName { get; }
        string LegislaturaURL { get; }

        string DeputadoTableName { get; }
        string DeputadoURL { get; }
        string DeputadoDetailWithIdArgURL { get; }

        string DespesaTableName { get; }
        string DespesaDetailWithIdMonthYeahArgURL { get; }
    }
}