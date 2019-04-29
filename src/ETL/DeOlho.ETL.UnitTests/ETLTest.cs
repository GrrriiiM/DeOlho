using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.ETL.Steps;
using DeOlho.ETL.Transforms;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Xunit;

namespace DeOlho.ETL.UnitTests
{
    public class ETLTest
    {
        [Fact]
        public async void Sources_HttpJsonSource()
        {
            var stringContent  = "[{'id':1, 'value':'Sucesso'}]";
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{'id':1, 'value':'Sucesso'}]"),
                })
                .Verifiable();

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);

            var uri = "http://teste.com/api";
            var httpJsonSource = new DeOlho.ETL.Sources.HttpJsonSource(httpClient, uri);
            var result = await httpJsonSource.Execute();
            
            result.Should().Be(stringContent);

            httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), // we expected a single external request
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get  // we expected a GET request
                    && req.RequestUri == new Uri($"{uri}") // to this uri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    
        [Fact]
        public async void Steps_Collection_DbCreateTableIfNotExistExtension_NotExists()
        {
            var tableName = "testeTable";
            var list = new[] { 
                new { 
                    _string = "string",
                    _short = new Int16(),
                    _int = 0,
                    _long = 0L,
                    _float = 0F,
                    _double = 0D,
                    _decimal = 0M,
                    _DateTime = new DateTime(),
                    _bool = false,
                    _shortNull = new Nullable<Int16>(),
                    _intNull = new Nullable<Int32>(),
                    _longNull = new Nullable<Int32>(),
                    _floatNull = new Nullable<Int32>(),
                    _doubleNull = new Nullable<Int32>(),
                    _decimalNull = new Nullable<Int32>(),
                    _DateTimeNull = new Nullable<DateTime>(),
                    _boolNull = new Nullable<Boolean>()
                }
            };
            var query = @"
                CREATE TABLE TESTETABLE (
                    _STRING VARCHAR(255) ,
                    _SHORT INT NOT NULL,
                    _INT INT NOT NULL,
                    _LONG INT NOT NULL,
                    _FLOAT DECIMAL(24,8) NOT NULL,
                    _DOUBLE DECIMAL(24,8) NOT NULL,
                    _DECIMAL DECIMAL(24,8) NOT NULL,
                    _DATETIME DATE NOT NULL,
                    _BOOL BOOLEAN NOT NULL,
                    _SHORTNULL INT ,
                    _INTNULL INT ,
                    _LONGNULL INT ,
                    _FLOATNULL INT ,
                    _DOUBLENULL INT ,
                    _DECIMALNULL INT ,
                    _DATETIMENULL DATE ,
                    _BOOLNULL BOOLEAN);";
            var dbConnectionMock = new Mock<IDbConnection>();
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbCommandMock = new Mock<IDbCommand>();
            var stepCollectionMock = new Mock<IStepCollection<object>>();

            dbConnectionMock
            .Setup(_=> _.CreateCommand())
            .Returns(dbCommandMock.Object);

            dbCommandMock.SetupAllProperties();


            dbCommandMock
            .Setup(_ => _.ExecuteScalar())
            .Returns(() => {
                if (dbCommandMock.Object.CommandText.ToUpper() == $"SELECT 1 FROM {tableName} WHERE 1 = 0")
                    throw new Exception();
                else
                    throw new Exception();
            });
            
            dbCommandMock
            .Setup(_ => _.ExecuteNonQuery())
            .Returns(() => {
                if (dbCommandMock.Object.CommandText.ToUpper().Replace(" ", "") == query.Replace(" ", "").Replace("\r", "").Replace("\n",""))
                    return 0;
                else
                    throw new System.Data.SyntaxErrorException();
            });

            stepCollectionMock
            .Setup(_ => _.Execute())
            .ReturnsAsync(list); 

            var step = DbCreateTableIfNotExistExtension.DbCreateTableIfNotExist(stepCollectionMock.Object, dbConnectionMock.Object, dbTransactionMock.Object, tableName);

            var result = await step.Execute();

            result.Should().BeEquivalentTo(list);
        }
    
        [Fact]
        public async void Transform_JsonToDynamicTransform()
        {
            var stepMock = new Mock<IStep<string>>();
            stepMock.Setup(_ => _.Execute())
            .ReturnsAsync(@"
                { 
                    'number': 1.2, 
                    'text': 'text', 
                    'date': '1999-12-31T23:59:01', 
                    'bit': true, 
                    'list': [
                        {
                            listNumber: 2.3
                        }
                    ]
                }");
            
            var transform = stepMock.Object.TransformJsonToDynamic()
                .Transform(async _ => await Task.Run(() => 
                    new {
                        Number = (decimal)_.number,
                        Text = (string)_.text,
                        Date = (DateTime)_.date,
                        Bit = (bool)_.bit,
                        List = new List<dynamic>(_.list).Select(_1 => (decimal)_1.listNumber)
                    }));

            var result = await transform.Execute();

            result.Number.Should().Be(1.2M);
            result.Text.Should().Be("text");
            result.Date.Year.Should().Be(1999);
            result.Date.Month.Should().Be(12);
            result.Date.Day.Should().Be(31);
            result.Date.Hour.Should().Be(23);
            result.Date.Minute.Should().Be(59);
            result.Date.Second.Should().Be(1);
            result.Bit.Should().Be(true);
            result.List.Should().HaveCount(1);
            result.List.Should().Contain(2.3M);

        }
    }
}
