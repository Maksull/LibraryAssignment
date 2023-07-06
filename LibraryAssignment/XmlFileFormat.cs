using System.Xml;
using LibraryAssignment.Models;

namespace LibraryAssignment
{
    public class XmlFileFormat : FileFormat
    {
        public XmlFileFormat()
        {
            Name = "xml";
        }

        public override List<Record> ReadFile(string filePath)
        {
            List<Record> records = new();

            XmlDocument xmlDoc = new();
            xmlDoc.Load(filePath);

            XmlNodeList? carNodes = xmlDoc.SelectNodes("//Car");

            if (carNodes is not null)
            {
                foreach (XmlNode carNode in carNodes)
                {
                    XmlNode? dateNode = carNode.SelectSingleNode("Date");
                    XmlNode? brandNode = carNode.SelectSingleNode("BrandName");
                    XmlNode? priceNode = carNode.SelectSingleNode("Price");

                    if (dateNode is null)
                        throw new ArgumentException("There is no date node in the node");
                    if (brandNode is null)
                        throw new ArgumentException("There is no brand node in the node");
                    if (priceNode is null)
                        throw new ArgumentException("There is no price node in the node");

                    DateTime date = DateTime.ParseExact(dateNode.InnerText, "dd.MM.yyyy", null);
                    string brand = brandNode.InnerText;
                    int price = int.Parse(priceNode.InnerText);

                    Record record = new(date, brand, price);
                    records.Add(record);
                }
            }

            return records;
        }

        public override void WriteFile(string filePath, List<Record> records)
        {
            XmlDocument xmlDoc = new();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDeclaration);

            XmlElement root = xmlDoc.CreateElement("Document");
            xmlDoc.AppendChild(root);

            foreach (Record record in records)
            {
                XmlElement carNode = xmlDoc.CreateElement("Car");
                root.AppendChild(carNode);

                XmlElement dateNode = xmlDoc.CreateElement("Date");
                dateNode.InnerText = record.Date.ToString("dd.MM.yyyy");
                carNode.AppendChild(dateNode);

                XmlElement brandNode = xmlDoc.CreateElement("BrandName");
                brandNode.InnerText = record.Brand;
                carNode.AppendChild(brandNode);

                XmlElement priceNode = xmlDoc.CreateElement("Price");
                priceNode.InnerText = record.Price.ToString();
                carNode.AppendChild(priceNode);
            }

            xmlDoc.Save(filePath);
        }
    }
}
