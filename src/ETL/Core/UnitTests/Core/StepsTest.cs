using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace DeOlho.ETL.UnitTests.Core
{
    public class StepsTest
    {
        public class Teste1
        {
            public int Id { get; set; }
            public string Value { get; set; }
            public DateTime Date { get; set; }
        }

        public class Teste2
        {
            public string Concat { get; set; }
        }
        
        [Fact]
        public async void StepTransform()
        {
            var teste1 = new Teste1{
                Id = 1,
                Value = "Value",
                Date = new DateTime(2000,1,1)
            };

            var stepMock = new Mock<IStep<Teste1>>();
            stepMock.Setup(_ => _.Execute())
                .ReturnsAsync(() => new StepValue<Teste1>(
                    teste1,
                    null));

            var stepTransform = new StepTransform<Teste1, Teste2>(stepMock.Object, 
                _ => Task.Run(() => new Teste2 { Concat = $"{_.Value.Id}{_.Value.Value}{_.Value.Date}" }));

            var result = await stepTransform.Execute();

            result.TypeValue.Should().Be(typeof(Teste2));
            result.Value.Concat.Should().Be($"{teste1.Id}{teste1.Value}{teste1.Date}");
            result.Parent.TypeValue.Should().Be(typeof(Teste1));
            ((Teste1)result.Parent.Value).Should().BeEquivalentTo(teste1);
        }

    }
}