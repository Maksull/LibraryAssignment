using FluentAssertions;

namespace LibraryAssignmentTests
{
    [TestFixture]
    public sealed class XmlFileFormatTests
    {
        [Test]
        public void ReadFile_ValidXml_ReturnsRecords()
        {
            // Arrange
            var fileFormat = new XmlFileFormat();
            string filePath = "xml-tests-source.xml";

            string workingDirectory = Environment.CurrentDirectory;
            string sourceFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{filePath}";

            var expectedRecords = new List<Record>
            {
                new(new DateTime(2008, 10, 10), "Alpha Romeo Brera", 37_000),
                new(new DateTime(2010, 5, 15), "BMW X5", 45_000),
                new(new DateTime(2012, 12, 20), "Mercedes-Benz E-Class", 55_000),
            };

            // Act
            List<Record> records = fileFormat.ReadFile(sourceFilePath);

            // Assert
            records.Should().BeEquivalentTo(expectedRecords);
        }

        [Test]
        public void WriteFile_ValidRecords_WritesXmlFile()
        {
            // Arrange
            var xmlFleFormat = new XmlFileFormat();
            var binFileFormat = new BinaryFileFormat();
            string source = "xml-tests-source.bin";
            string target = "xml-tests-target.xml";

            string workingDirectory = Environment.CurrentDirectory;
            string sourceFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{source}";
            string targetFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{target}";

            var expectedRecords = new List<Record>
            {
                new(new DateTime(2008, 10, 10), "Alpha Romeo Brera", 37_000),
                new(new DateTime(2010, 5, 15), "BMW X5", 45_000),
                new(new DateTime(2012, 12, 20), "Mercedes-Benz E-Class", 55_000),
            };

            // Act
            List<Record> sourceRecords = binFileFormat.ReadFile(sourceFilePath);
            xmlFleFormat.WriteFile(targetFilePath, sourceRecords);

            List<Record> targetRecords = xmlFleFormat.ReadFile(targetFilePath);

            // Assert
            (File.Exists(targetFilePath)).Should().BeTrue();
            targetRecords.Should().BeEquivalentTo(expectedRecords);
        }
    }
}
