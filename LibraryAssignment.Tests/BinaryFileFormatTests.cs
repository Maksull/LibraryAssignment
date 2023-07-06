using FluentAssertions;

namespace LibraryAssignmentTests
{
    [TestFixture]
    public sealed class BinaryFileFormatTests
    {
        [Test]
        public void ReadFile_ValidXml_ReturnsRecords()
        {
            // Arrange
            var fileFormat = new BinaryFileFormat();
            string filePath = "bin-tests-source.bin";

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
            var binaryFileFormat = new BinaryFileFormat();
            var xmlFileFormat = new XmlFileFormat();
            string source = "bin-tests-source.xml";
            string target = "bin-tests-target.bin";

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
            List<Record> sourceRecords = xmlFileFormat.ReadFile(sourceFilePath);
            binaryFileFormat.WriteFile(targetFilePath, sourceRecords);

            List<Record> targetRecords = binaryFileFormat.ReadFile(targetFilePath);

            // Assert
            (File.Exists(targetFilePath)).Should().BeTrue();
            targetRecords.Should().BeEquivalentTo(expectedRecords);
        }
    }
}
