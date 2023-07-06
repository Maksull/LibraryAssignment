using FluentAssertions;

namespace LibraryAssignmentTests
{
    [TestFixture]
    public sealed class FileFormatConverterTests
    {
        [Test]
        public void Convert_ValidFormats_Converts_ToBin_Successfully()
        {
            // Arrange
            var converter = new FileFormatConverter();
            var sourceFormat = new XmlFileFormat();
            var targetFormat = new BinaryFileFormat();
            converter.AddFormat(sourceFormat);
            converter.AddFormat(targetFormat);

            string sourceFile = "source.xml";
            string targetFile = "target.bin";
            string sourceFormatName = "xml";
            string targetFormatName = "bin";

            string workingDirectory = Environment.CurrentDirectory;
            string sourceFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{sourceFile}";
            string targetFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{targetFile}";

            // Act
            converter.Convert(sourceFilePath, targetFilePath, sourceFormatName, targetFormatName);
            List<Record> sourceRecords = sourceFormat.ReadFile(sourceFilePath);
            List<Record> targetRecords = targetFormat.ReadFile(targetFilePath);

            // Assert
            sourceRecords.Should().BeEquivalentTo(targetRecords);
        }

        [Test]
        public void Convert_ValidFormats_Converts_ToXml_Successfully()
        {
            // Arrange
            var converter = new FileFormatConverter();
            var sourceFormat = new BinaryFileFormat();
            var targetFormat = new XmlFileFormat();
            converter.AddFormat(sourceFormat);
            converter.AddFormat(targetFormat);

            string sourceFile = "source.bin";
            string targetFile = "target.xml";
            string sourceFormatName = "bin";
            string targetFormatName = "xml";

            string workingDirectory = Environment.CurrentDirectory;
            string sourceFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{sourceFile}";
            string targetFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{targetFile}";

            // Act
            converter.Convert(sourceFilePath, targetFilePath, sourceFormatName, targetFormatName);
            List<Record> sourceRecords = sourceFormat.ReadFile(sourceFilePath);
            List<Record> targetRecords = targetFormat.ReadFile(targetFilePath);

            // Assert
            sourceRecords.Should().BeEquivalentTo(targetRecords);
        }


        [Test]
        public void Convert_UnsupportedSourceFormat_ThrowsArgumentException()
        {
            // Arrange
            var converter = new FileFormatConverter();
            var sourceFormat = new XmlFileFormat();
            var targetFormat = new BinaryFileFormat();
            converter.AddFormat(targetFormat);

            string sourceFile = "source.xml";
            string targetFile = "target.bin";
            string sourceFormatName = "xml";
            string targetFormatName = "Binary";

            // Act and Assert
            Assert.Throws<ArgumentException>(() => converter.Convert(sourceFile, targetFile, sourceFormatName, targetFormatName));
        }

        [Test]
        public void Convert_UnsupportedTargetFormat_ThrowsArgumentException()
        {
            // Arrange
            var converter = new FileFormatConverter();
            var sourceFormat = new XmlFileFormat();
            var targetFormat = new BinaryFileFormat();
            converter.AddFormat(sourceFormat);

            string sourceFile = "source.xml";
            string targetFile = "target.bin";
            string sourceFormatName = "XML";
            string targetFormatName = "binary";

            // Act and Assert
            Assert.Throws<ArgumentException>(() => converter.Convert(sourceFile, targetFile, sourceFormatName, targetFormatName));
        }
    }
}