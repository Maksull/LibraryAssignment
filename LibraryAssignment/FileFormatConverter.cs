using LibraryAssignment.Models;

namespace LibraryAssignment
{
    public sealed class FileFormatConverter
    {
        private List<FileFormat> _supportedFormats;

        public FileFormatConverter()
        {
            _supportedFormats = new List<FileFormat>();
        }

        public void AddFormat(FileFormat format)
        {
            _supportedFormats.Add(format);
        }

        public void RemoveFormat(FileFormat format)
        {
            _supportedFormats.Remove(format);
        }

        public FileFormat? GetFormat(string formatName)
        {
            return _supportedFormats.Find(f => f.Name == formatName);
        }

        public void Convert(string sourceFile, string targetFile, string sourceFormatName, string targetFormatName)
        {
            FileFormat? sourceFormat = GetFormat(sourceFormatName);
            FileFormat? targetFormat = GetFormat(targetFormatName);

            if (sourceFormat == null)
                throw new ArgumentException($"Unsupported source format: {sourceFormatName}");

            if (targetFormat == null)
                throw new ArgumentException($"Unsupported target format: {targetFormatName}");

            // Read source file
            List<Record> records = sourceFormat.ReadFile(sourceFile);

            // Write records to target file
            targetFormat.WriteFile(targetFile, records);
        }
    }
}
