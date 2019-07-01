using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.Services.Despesas.Domain;
using DeOlho.Services.Despesas.Domain.SeedWork;
using DeOlho.Services.Despesas.Infrastucture.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.Services.Despesas.Application.Commands
{
    public class CreateDespesasCommand : IRequest, IHasCPF, IHasAno, IHasMes, IHasTipoId
    {
        public long CPF { get; set; }
        public int TipoId { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public decimal Valor { get; set; }
    }

    public class CreateDespesasCommandHandler : IRequestHandler<CreateDespesasCommand>
    {
        readonly DeOlhoDbContext _deOlhoDbContext;

        public CreateDespesasCommandHandler(DeOlhoDbContext deOlhoDbContext)
        {
            _deOlhoDbContext = deOlhoDbContext;
        } 

        public async Task<Unit> Handle(CreateDespesasCommand request, CancellationToken cancellationToken)
        {   
            var sqlStringBuilder = new StringBuilder();

            var despesas = typeof(TotalAno).Assembly.GetTypes().Where(_ => 
                typeof(IHasValor).IsAssignableFrom(_) && _.IsClass);

            var sqlModel = @"
            INSERT INTO {0} ({2}, Valor) VALUES({3}, {1}) ON DUPLICATE KEY UPDATE Valor = Valor + {1};";

            var parameters = new Dictionary<string, string>
            {
                { nameof(request.CPF), request.CPF.ToString()},
                { nameof(request.TipoId), request.TipoId.ToString()},
                { nameof(request.Ano), request.Ano.ToString()},
                { nameof(request.Mes), request.Mes.ToString()}
            };

            var valor = request.Valor.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));

            foreach(var despesa in despesas)
            {
                var model = _deOlhoDbContext.Model.FindEntityType(despesa);
                var tableName = model.Relational().TableName;
                var columnValues = new Dictionary<string, string>();
                
                foreach(var parameter in parameters)
                {
                    var column = model.GetProperties().SingleOrDefault(_ => _.PropertyInfo.Name == parameter.Key);
                    if (column != null)
                        columnValues.Add(column.Name, parameter.Value);
                }
                
                var where = string.Join(" AND ", columnValues.Select(_ => $"{_.Key} = {_.Value}"));
                var update = string.Join(", ", columnValues.Select(_ => $"{_.Key} = {_.Value}"));
                var insertColumns = string.Join(", ", columnValues.Keys);
                var insertValues = string.Join(", ", columnValues.Values);
                
                
                sqlStringBuilder.AppendLine(string.Format(sqlModel, tableName, valor, insertColumns, insertValues, update, where));
                sqlStringBuilder.AppendLine();

            }

            using (var transaction = await _deOlhoDbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                var sql = sqlStringBuilder.ToString();
                await _deOlhoDbContext.Database.ExecuteSqlCommandAsync(sql);
                transaction.Commit();
            }

            return await Unit.Task;
        }
    }
}