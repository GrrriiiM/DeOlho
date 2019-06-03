using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace DeOlho.ETL.tse_jus_br.UnitTest
{
    public class Services
    {
        [Fact]
        public async void Politico_ExecuteETL()
        {
            await Task.CompletedTask;
            1.Should().Be(2);
        }
    }
}
