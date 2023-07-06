using LibraryAssignment.Models;

namespace LibraryAssignment
{
    public class BinaryFileFormat : FileFormat
    {
        public BinaryFileFormat()
        {
            Name = "bin";
        }

        public override List<Record> ReadFile(string filePath)
        {
            List<Record> records = new();

            using (BinaryReader reader = new(File.OpenRead(filePath)))
            {
                // Read header
                ushort header = reader.ReadUInt16();
                if (header != 0x2526)
                    throw new InvalidDataException("Invalid file header");


                // Read records count
                int recordCount = reader.ReadInt32();
                if (recordCount < 0)
                    throw new InvalidDataException("Invalid record count");

                for (int i = 0; i < recordCount; i++)
                {
                    // Read date
                    string dateString = reader.ReadString();
                    DateTime date = DateTime.ParseExact(dateString, "ddMMyyyy", null);

                    // Read brand name
                    ushort brandNameLength = reader.ReadUInt16();
                    string brandName = reader.ReadString();

                    // Read price
                    int price = reader.ReadInt32();

                    Record record = new(date, brandName, price);
                    records.Add(record);
                }
            }

            return records;
        }

        public override void WriteFile(string filePath, List<Record> records)
        {
            using (BinaryWriter writer = new(File.OpenWrite(filePath)))
            {
                // Write header
                writer.Write((ushort)0x2526);

                // Write records count
                writer.Write(records.Count);

                foreach (Record record in records)
                {
                    // Write date
                    string dateString = record.Date.ToString("ddMMyyyy");
                    writer.Write(dateString);

                    // Write brand name
                    writer.Write((ushort)record.Brand.Length);
                    writer.Write(record.Brand);

                    // Write price
                    writer.Write(record.Price);
                }
            }
        }
    }
}
