namespace LibraryAssignment.Models
{
    public abstract class FileFormat
    {
        public string Name { get; protected set; } = string.Empty;

        public abstract List<Record> ReadFile(string filePath);

        public abstract void WriteFile(string filePath, List<Record> records);

    }
}
