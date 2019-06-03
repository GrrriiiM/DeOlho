using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using DeOlho.ETL;
using DeOlho.ETL.Transforms;
using FluentAssertions;
using Moq;
using Xunit;

namespace UnitTests.Core
{
    public class TransformsTest
    {
        
        [Fact]
        public async void DescompressStream()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var demoFile = archive.CreateEntry("foo.txt");

                    using (var entryStream = demoFile.Open())
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        streamWriter.Write("Bar!");
                    }
                }

                var listStepValue = new List<StepValue<Stream>>() { new StepValue<Stream>(memoryStream, null) };

                var stepCollectionMock = new Mock<StepCollection<Stream>>();
                stepCollectionMock.Setup(_ => _.Execute())
                .ReturnsAsync(listStepValue);

                var result = await DescompressStreamTransformExtensions.TransformDescompressStream(stepCollectionMock.Object).Execute();

                result.Should().HaveCount(1);
                var streamDecompresseds = result.ToList()[0].Value;
                streamDecompresseds.Should().HaveCount(1);
                var streamDecompressed = streamDecompresseds[0];
                streamDecompressed.Name.Should().Be("foo.txt");
                var reader = new StreamReader(streamDecompressed.Stream);
                var text = reader.ReadToEnd();
                text.Should().Be("Bar!");
            }
        }

        [Fact]
        public async void CsvToDynamic()
        {
            var csv = "id,name\r\n1,teste1\r\n2,teste2";
            using(var ms = new MemoryStream())
            {
                using(var sw = new StreamWriter(ms))
                {
                    sw.Write(csv);
                    sw.Flush();
                    ms.Position = 0;
                    var listStepValue = new List<StepValue<Stream>>() { new StepValue<Stream>(ms, null) };

                    var stepCollectionMock = new Mock<StepCollection<Stream>>();
                    stepCollectionMock.Setup(_ => _.Execute())
                    .ReturnsAsync(listStepValue);
                    var result = (await CsvToDynamicTransformExtensions.TransformCsvToDynamic(stepCollectionMock.Object).Execute()).ToList();

                    result.Should().HaveCount(1);
                    var list = result[0].Value.ToList();
                    list.Should().HaveCount(2);
                    var item1 = list[0];
                    var item2 = list[1];
                    ((string)item1.id).Should().Be("1");
                    ((string)item1.name).Should().Be("teste1");
                    ((string)item2.id).Should().Be("2");
                    ((string)item2.name).Should().Be("teste2");
                }
            }
        }

        [Fact]
        public async void CsvToObject()
        {
            await Task.CompletedTask;
            "A".Should().Be("B");
        }

    }
}