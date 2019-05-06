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
        private dynamic objectTest1;

        private dynamic objectTest2;

        public ETLTest()
        {
            objectTest1 = new { 
                _string = "string",
                _short = new Int16(),
                _int = 1,
                _long = 2L,
                _float = 3.4F,
                _double = 4.5D,
                _decimal = 5.6M,
                _DateTime = new DateTime(1999, 12, 31, 23, 59, 01),
                _bool = true,
                _shortNull = new Nullable<Int16>(),
                _intNull = new Nullable<Int32>(),
                _longNull = new Nullable<Int64>(),
                _floatNull = new Nullable<Single>(),
                _doubleNull = new Nullable<double>(),
                _decimalNull = new Nullable<decimal>(),
                _DateTimeNull = new Nullable<DateTime>(),
                _boolNull = new Nullable<Boolean>()
            };
            objectTest2 = new { 
                _string = "string1",
                _short = 9,
                _int = 8,
                _long = 7L,
                _float = 6.51F,
                _double = 5.41D,
                _decimal = 4.31M,
                _DateTime = new DateTime(2099, 01, 15, 5, 5, 5),
                _bool = false,
                _shortNull = new Nullable<Int16>(1000),
                _intNull = new Nullable<Int32>(2000),
                _longNull = new Nullable<Int64>(3000),
                _floatNull = new Nullable<Single>(4000.44F),
                _doubleNull = new Nullable<double>(5000.55D),
                _decimalNull = new Nullable<decimal>(6000.66M),
                _DateTimeNull = new Nullable<DateTime>(new DateTime(1901,10,20,1,2,3)),
                _boolNull = new Nullable<Boolean>(true)
            };
        }

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
            var tableName = "TESTETABLE";
            var list = new[] { 
                objectTest1
            };
            var queryVerifyTable = $"SELECT 1 FROM {tableName} WHERE 1 = 0";
            var queryCreateTable = new string[] {
                $"CREATE TABLE {tableName}",
                "_STRING VARCHAR(255)",
                "_SHORT INT NOT NULL",
                "_INT INT NOT NULL",
                "_LONG INT NOT NULL",
                "_FLOAT DECIMAL(24,8) NOT NULL",
                "_DOUBLE DECIMAL(24,8) NOT NULL",
                "_DECIMAL DECIMAL(24,8) NOT NULL",
                "_DATETIME DATE NOT NULL",
                "_BOOL BOOLEAN NOT NULL",
                "_SHORTNULL INT",
                "_INTNULL INT",
                "_LONGNULL INT",
                "_FLOATNULL DECIMAL(24,8)",
                "_DOUBLENULL DECIMAL(24,8)",
                "_DECIMALNULL DECIMAL(24,8)",
                "_DATETIMENULL DATE",
                "_BOOLNULL BOOLEAN"
            };
            var dbConnectionMock = new Mock<IDbConnection>();
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbCommandMock = new Mock<IDbCommand>();
            var stepCollectionMock = new Mock<IStepCollection<object>>();

            dbCommandMock.SetupAllProperties();

            dbConnectionMock
            .Setup(_=> _.CreateCommand())
            .Returns(dbCommandMock.Object);

            dbCommandMock
            .Setup(_ => _.ExecuteScalar())
            .Returns(() => {
                throw new Exception();
            });
            
            dbCommandMock
            .Setup(_ => _.ExecuteNonQuery())
            .Returns(() => {
                return 0;
            });

            stepCollectionMock
            .Setup(_ => _.Execute())
            .ReturnsAsync(list.Select(_ => new StepValue<dynamic>(_ , null))); 

            var step = DbCreateTableIfNotExistExtension.DbCreateTableIfNotExist(stepCollectionMock.Object, dbConnectionMock.Object, dbTransactionMock.Object, tableName);

            var result = (await step.Execute()).Select(_ => _.Value);

            dbCommandMock.VerifySet(_ => _.CommandText = queryVerifyTable, Times.Once);

            dbCommandMock.VerifySet(_ => _.CommandText = It.Is<string>(_1 => queryCreateTable.All(_2 => _1.ToUpper().Contains(_2))), Times.Once);

            result.Should().BeEquivalentTo(list);
        }
    
        [Fact]
        public async void Steps_Collection_DbCreateTableIfNotExistExtension_Exists()
        {
            var tableName = "TESTETABLE";
            var list = new[] { 
                objectTest1
            };
            var queryVerifyTable = $"SELECT 1 FROM {tableName} WHERE 1 = 0";

            var dbConnectionMock = new Mock<IDbConnection>();
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbCommandMock = new Mock<IDbCommand>();
            var stepCollectionMock = new Mock<IStepCollection<object>>();

            dbCommandMock.SetupAllProperties();

            dbConnectionMock
            .Setup(_=> _.CreateCommand())
            .Returns(dbCommandMock.Object);

            dbCommandMock
            .Setup(_ => _.ExecuteScalar())
            .Returns(() => {
                return 0;
            });
            
            dbCommandMock
            .Setup(_ => _.ExecuteNonQuery())
            .Returns(() => {
                return 0;
            });

            stepCollectionMock
            .Setup(_ => _.Execute())
            .ReturnsAsync(list.Select(_ => new StepValue<dynamic>(_, null))); 

            var step = DbCreateTableIfNotExistExtension.DbCreateTableIfNotExist(stepCollectionMock.Object, dbConnectionMock.Object, dbTransactionMock.Object, tableName);

            var result = (await step.Execute()).Select(_ => _.Value);

            dbCommandMock.VerifySet(_ => _.CommandText = It.Is<string>(_1 => _1.ToUpper() == queryVerifyTable.ToUpper()), Times.Once);

            dbCommandMock.VerifySet(_ => _.CommandText = It.Is<string>(_1 => _1.ToUpper() != queryVerifyTable.ToUpper()), Times.Never);

            result.Should().BeEquivalentTo(list);
        }

        [Fact]
        public async void Steps_Collection_DbCreateTableIfNotExistExtension_Empty()
        {
            var tableName = "TESTETABLE";
            var list = new object[0];
            
            
            var dbConnectionMock = new Mock<IDbConnection>();
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbCommandMock = new Mock<IDbCommand>();
            var stepCollectionMock = new Mock<IStepCollection<object>>();

            dbCommandMock.SetupAllProperties();

            dbConnectionMock
            .Setup(_=> _.CreateCommand())
            .Returns(dbCommandMock.Object);

            dbCommandMock
            .Setup(_ => _.ExecuteScalar())
            .Returns(() => {
                return 0;
            });
            
            dbCommandMock
            .Setup(_ => _.ExecuteNonQuery())
            .Returns(() => {
                return 0;
            });

            stepCollectionMock
            .Setup(_ => _.Execute())
            .ReturnsAsync(list.Select(_ => new StepValue<object>(_, null))); 

            var step = DbCreateTableIfNotExistExtension.DbCreateTableIfNotExist(stepCollectionMock.Object, dbConnectionMock.Object, dbTransactionMock.Object, tableName);

            var result = await step.Execute();

            dbCommandMock.VerifySet(_ => _.CommandText = It.IsAny<string>(), Times.Never);

            result.Should().BeEquivalentTo(list);
        }

        [Fact]
        public async void Transform_JsonToDynamicTransform()
        {
            var stepMock = new Mock<IStep<string>>();
            stepMock.Setup(_ => _.Execute())
            .ReturnsAsync(new StepValue<string>(@"
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
                }", null));
            
            var transform = stepMock.Object.TransformJsonToDynamic()
                .Transform(_ => 
                    new {
                        Number = (decimal)_.Value.number,
                        Text = (string)_.Value.text,
                        Date = (DateTime)_.Value.date,
                        Bit = (bool)_.Value.bit,
                        List = new List<dynamic>(_.Value.list).Select(_1 => (decimal)_1.listNumber)
                    });

            var result = (await transform.Execute()).Value;

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
    
        [Fact]
        public async void Transform_Collection_JsonToDynamicTransform()
        {
            var stepMock = new Mock<IStepCollection<string>>();
            stepMock.Setup(_ => _.Execute())
            .ReturnsAsync(new string [] {@"
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
                }",
                @"{ 
                    'number': 9876.5432, 
                    'text': 'text1', 
                    'date': '2010-10-20T10:11:12', 
                    'bit': false, 
                    'list': [
                        {
                            listNumber: 1234.5678
                        }
                    ]
                }"}.Select(_ => new StepValue<string>(_, null)));
            
            var transform = stepMock.Object.TransformJsonToDynamic()
                .Transform(_ =>  
                    new {
                        Number = (decimal)_.Value.number,
                        Text = (string)_.Value.text,
                        Date = (DateTime)_.Value.date,
                        Bit = (bool)_.Value.bit,
                        List = new List<dynamic>(_.Value.list).Select(_1 => (decimal)_1.listNumber)
                    });

            var result = (await transform.Execute()).Select(_ => _.Value).ToArray();


            result.Should().HaveCount(2);

            result[0].Number.Should().Be(1.2M);
            result[0].Text.Should().Be("text");
            result[0].Date.Year.Should().Be(1999);
            result[0].Date.Month.Should().Be(12);
            result[0].Date.Day.Should().Be(31);
            result[0].Date.Hour.Should().Be(23);
            result[0].Date.Minute.Should().Be(59);
            result[0].Date.Second.Should().Be(1);
            result[0].Bit.Should().Be(true);
            result[0].List.Should().HaveCount(1);
            result[0].List.Should().Contain(2.3M);

            result[1].Number.Should().Be(9876.5432M);
            result[1].Text.Should().Be("text1");
            result[1].Date.Year.Should().Be(2010);
            result[1].Date.Month.Should().Be(10);
            result[1].Date.Day.Should().Be(20);
            result[1].Date.Hour.Should().Be(10);
            result[1].Date.Minute.Should().Be(11);
            result[1].Date.Second.Should().Be(12);
            result[1].Bit.Should().Be(false);
            result[1].List.Should().HaveCount(1);
            result[1].List.Should().Contain(1234.5678M);


        }
    

        [Fact]
        public async void DbDestination_Collection_DbDestination()
        {
            var tableName = "TESTETABLE";
            var list = new[] { 
                objectTest1,
                objectTest2
            };

            var stepMock = new Mock<IStepCollection<object>>();
            stepMock.Setup(_ => _.Execute())
            .ReturnsAsync(list.Select(_ => new StepValue<dynamic>(_, null)));

            var queryInsertTable = new string[] {
                $"INSERT INTO {tableName}",
                "_STRING", "'STRING'", "'STRING1'",
                "_SHORT", "0", "9",
                "_INT", "1", "8",
                "_LONG", "2", "7",
                "_FLOAT", "3.4", "6.51",
                "_DOUBLE", "4.5", "5.41",
                "_DECIMAL", "5.6", "4.31",
                "_DATETIME", "'1999-12-31 23:59:01'", "'2099-01-15 05:05:05'",
                "_BOOL","TRUE", "FALSE",
                "_SHORTNULL", "NULL", "1000",
                "_INTNULL", "2000",
                "_LONGNULL", "3000",
                "_FLOATNULL", "4000.44",
                "_DOUBLENULL", "5000.55",
                "_DECIMALNULL", "6000.66",
                "_DATETIMENULL", "'1901-10-20 01:02:03'",
                "_BOOLNULL"
            };
            var dbConnectionMock = new Mock<IDbConnection>();
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbCommandMock = new Mock<IDbCommand>();

            dbCommandMock.SetupAllProperties();

            dbConnectionMock
            .Setup(_=> _.CreateCommand())
            .Returns(dbCommandMock.Object);

            dbCommandMock
            .Setup(_ => _.ExecuteScalar())
            .Returns(() => {
                throw new Exception();
            });
            
            dbCommandMock
            .Setup(_ => _.ExecuteNonQuery())
            .Returns(() => {
                return 0;
            });

            var result = (await new Destinations.DbDestinationCollection(dbConnectionMock.Object, dbTransactionMock.Object, tableName)
                .Execute(stepMock.Object))
                .Select(_ => _.Value);

            dbCommandMock.VerifySet(_ => _.CommandText = It.Is<string>(_1 => queryInsertTable.All(_2 => _1.ToUpper().Contains(_2.ToUpper()))), Times.Once);

            result.Should().BeEquivalentTo(list);
        }

        [Fact]
        public async void Step_Extract_Load()
        {
            var stepMock = new Mock<Step<int>>();
            stepMock.Setup(_ => _.Execute())
            .ReturnsAsync(new StepValue<int>(1, null));

            var sourceMock = new Mock<ISource<string>>();
            sourceMock.Setup(_ => _.Execute())
            .ReturnsAsync("A");
            
            var result = (await stepMock.Object
            .Extract(_ => sourceMock.Object)
            .Load()).Value;

            result.Should().Be("A");

        }



        [Fact]
        public async void Step_Collection_Extract_Load()
        {
            var stepCollectionMock = new Mock<StepCollection<int>>();
            stepCollectionMock.Setup(_ => _.Execute())
            .ReturnsAsync((new int[] {
                1,2,3,4,5
            }).Select(_ => new StepValue<int>(_, null)));

            var sourceCollectionMock = new Mock<ISource<string>>();
            sourceCollectionMock.Setup(_ => _.Execute())
            .ReturnsAsync("A");
            
            var result = (await stepCollectionMock.Object
            .Extract(_ => sourceCollectionMock.Object)
            .Load()).Select(_ => _.Value);

            result.Should().HaveCount(5);
            result.Should().OnlyContain(_ => _ == "A");

        }

        [Fact]
        public async void Process_Extract_Transform_TransformAsync_TransformList_Collection_Transform_TransformAsync_Load()
        {
            var sourceMock = new Mock<ISource<dynamic>>();
            sourceMock.Setup(_ => _.Execute())
            .ReturnsAsync(new {
                Value = 1,
                List = new int[] {
                    1,2,3,4,5
                }
            });
            
            var result = (await new Process()
            .Extract(() => sourceMock.Object)
            .Extract(_ => sourceMock.Object)
            .Transform(_ => _.Value)
            .TransformAsync(async _ => await Task.Run(() => _.Value))
            .TransformToList(_ => new List<int>(_.Value.List))
            .Transform(_ => (decimal)_.Value)
            .TransformAsync(async _ => await Task.Run(() => (_.Value * 2) + (_.Value/10)))
            .Load()).Select(_ => _.Value);

            result.Should().HaveCount(5);
            result.Should().Contain(2.1M);
            result.Should().Contain(4.2M);
            result.Should().Contain(6.3M);
            result.Should().Contain(8.4M);
            result.Should().Contain(10.5M);

        }

        [Fact]
        public async void Step_Collection_DbDelete()
        {
            var tableName = "TESTETABLE";
            var queryDeleteTable = $"DELETE FROM {tableName} WHERE ID > 1 AND TEXTO = 'TESTE' AND DATA<='2019-10-12'";
            var dbConnectionMock = new Mock<IDbConnection>();
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbCommandMock = new Mock<IDbCommand>();
            var stepCollectionMock = new Mock<IStepCollection<int>>();

            dbCommandMock.SetupAllProperties();

            dbConnectionMock
            .Setup(_=> _.CreateCommand())
            .Returns(dbCommandMock.Object);

            
            dbCommandMock
            .Setup(_ => _.ExecuteNonQuery())
            .Returns(() => {
                return 0;
            });

            stepCollectionMock
            .Setup(_ => _.Execute())
            .ReturnsAsync(new StepValue<int>[] {}); 

            var step = DbDeleteStepCollectionExtension.DbDelete(stepCollectionMock.Object, dbConnectionMock.Object, dbTransactionMock.Object, tableName, "ID > 1 AND TEXTO = 'TESTE' AND DATA <= '2019-10-12'");

            var result = (await step.Execute()).Select(_ => _.Value);

            dbCommandMock.VerifySet(_ => _.CommandText = It.Is<string>(_1 => _1.ToUpper().Replace(" ", "") == queryDeleteTable.ToUpper().Replace(" ", "")), Times.Once);
        }
    }
}
