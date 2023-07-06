using LibraryAssignment;
using LibraryAssignment.Models;

var xmlFileFormat = new XmlFileFormat();
var binaryFileFormat = new BinaryFileFormat();

string[] options = { "Read", "Create", "Update", "Delete", "Transfer to", "Exit" };
int selectedIndex = 0;

Console.CursorVisible = false;

while (true)
{
    Console.Clear();
    Console.WriteLine($"Choose action");
    for (int i = 0; i < options.Length; i++)
    {
        if (i == selectedIndex)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-> " + options[i]);
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("   " + options[i]);
        }
    }

    ConsoleKeyInfo keyInfo = Console.ReadKey();

    if (keyInfo.Key == ConsoleKey.UpArrow)
    {
        selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
    }
    else if (keyInfo.Key == ConsoleKey.DownArrow)
    {
        selectedIndex = (selectedIndex + 1) % options.Length;
    }
    else if (keyInfo.Key == ConsoleKey.Enter)
    {
        Console.Clear();
        Console.WriteLine("You selected: " + options[selectedIndex]);

        switch (selectedIndex)
        {
            case 0:
                ReadRecords();
                break;
            case 1:
                CreateRecord();
                break;
            case 2:
                UpdateRecord();
                break;
            case 3:
                DeleteRecord();
                break;
            case 4:
                TransferTo();
                break;
            case 5:
                Console.WriteLine("Tschuss");
                break;
        }
        if (selectedIndex != 5)
        {
            PressAnyKeyToBack();
        }
        else if (selectedIndex == 5)
        {
            break;
        }
    }
}

void ReadRecords()
{
    string sourceFormat = GetDataType("source");

    Console.Write("Enter a source file's name: ");
    string sourceFileName = Console.ReadLine();

    string workingDirectory = Environment.CurrentDirectory;
    string sourceFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{sourceFileName}.{sourceFormat}";

    Console.Clear();
    Console.WriteLine("Source: " + sourceFilePath);

    List<Record> records = new();

    if (sourceFormat == "xml")
    {
        records = xmlFileFormat.ReadFile(sourceFilePath);
    }
    else if (sourceFormat == "bin")
    {
        records = binaryFileFormat.ReadFile(sourceFilePath);
    }

    DisplayRecords(records);
}

void CreateRecord()
{
    string sourceFormat = GetDataType("source");

    Console.Write("Enter a source file's name: ");
    string sourceFileName = Console.ReadLine();

    string workingDirectory = Environment.CurrentDirectory;
    string sourceFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{sourceFileName}.{sourceFormat}";

    Console.Clear();
    Console.WriteLine("Source: " + sourceFilePath);

    List<Record> records = new();

    if (sourceFormat == "xml")
    {
        records = xmlFileFormat.ReadFile(sourceFilePath);
    }
    else if (sourceFormat == "bin")
    {
        records = binaryFileFormat.ReadFile(sourceFilePath);
    }

    DisplayRecords(records);

    Console.WriteLine("Create a New Record");
    Console.WriteLine("-------------------");

    Console.Write("Enter the date (MM/dd/yyyy): ");
    DateTime date = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);

    Console.Write("Enter the brand: ");
    string brand = Console.ReadLine();

    Console.Write("Enter the price: ");
    int price = int.Parse(Console.ReadLine());

    Record record = new(date, brand, price);

    records.Add(record);

    if (sourceFormat == "xml")
    {
        xmlFileFormat.WriteFile(sourceFilePath, records);
    }
    else if (sourceFormat == "bin")
    {
        binaryFileFormat.WriteFile(sourceFilePath, records);
    }

    DisplayRecords(records);
}

void UpdateRecord()
{
    string sourceFormat = GetDataType("source");

    Console.Write("Enter a source file's name: ");
    string sourceFileName = Console.ReadLine();

    string workingDirectory = Environment.CurrentDirectory;
    string sourceFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{sourceFileName}.{sourceFormat}";

    Console.Clear();
    Console.WriteLine("Source: " + sourceFilePath);

    List<Record> records = new();

    if (sourceFormat == "xml")
    {
        records = xmlFileFormat.ReadFile(sourceFilePath);
    }
    else if (sourceFormat == "bin")
    {
        records = binaryFileFormat.ReadFile(sourceFilePath);
    }

    int selectedIndex = 0;

    while (true)
    {
        Console.Clear();
        Console.WriteLine($"Update a record");
        for (int i = 0; i < records.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-> " + records[i]);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("   " + records[i]);
            }
        }

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            selectedIndex = (selectedIndex - 1 + records.Count) % records.Count;
        }
        else if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            selectedIndex = (selectedIndex + 1) % records.Count;
        }
        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            Console.WriteLine("You selected: " + records[selectedIndex]);

            break;
        }
    }

    DisplayRecord(records[selectedIndex]);

    Console.WriteLine("Create a data for the Record");
    Console.WriteLine("-------------------");

    Console.Write("Enter the date (MM/dd/yyyy): ");
    DateTime date = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);

    Console.Write("Enter the brand: ");
    string brand = Console.ReadLine();

    Console.Write("Enter the price: ");
    int price = int.Parse(Console.ReadLine());

    records.RemoveAt(selectedIndex);
    records.Insert(selectedIndex, new Record(date, brand, price));

    if (sourceFormat == "xml")
    {
        xmlFileFormat.WriteFile(sourceFilePath, records);
    }
    else if (sourceFormat == "bin")
    {
        binaryFileFormat.WriteFile(sourceFilePath, records);
    }

    DisplayRecords(records);
}

void DeleteRecord()
{
    string sourceFormat = GetDataType("source");

    Console.Write("Enter a source file's name: ");
    string sourceFileName = Console.ReadLine();

    string workingDirectory = Environment.CurrentDirectory;
    string sourceFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{sourceFileName}.{sourceFormat}";

    Console.Clear();
    Console.WriteLine("Source: " + sourceFilePath);

    List<Record> records = new();

    if (sourceFormat == "xml")
    {
        records = xmlFileFormat.ReadFile(sourceFilePath);
    }
    else if (sourceFormat == "bin")
    {
        records = binaryFileFormat.ReadFile(sourceFilePath);
    }

    int selectedIndex = 0;

    while (true)
    {
        Console.Clear();
        Console.WriteLine($"Delete a record");
        for (int i = 0; i < records.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("-> " + records[i]);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("   " + records[i]);
            }
        }

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            selectedIndex = (selectedIndex - 1 + records.Count) % records.Count;
        }
        else if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            selectedIndex = (selectedIndex + 1) % records.Count;
        }
        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            Console.WriteLine("You selected: " + records[selectedIndex]);

            break;
        }
    }

    records.RemoveAt(selectedIndex);

    if (sourceFormat == "xml")
    {
        xmlFileFormat.WriteFile(sourceFilePath, records);
    }
    else if (sourceFormat == "bin")
    {
       binaryFileFormat.WriteFile(sourceFilePath, records);
    }

    DisplayRecords(records);
}

void TransferTo()
{
    string sourceFormat = GetDataType("source");

    Console.Write("Enter a source file's name: ");
    string sourceFileName = Console.ReadLine();

    string targetFormat = GetDataType("target");

    Console.Write("Enter a target file's name: ");
    string targetFileName = Console.ReadLine();

    string workingDirectory = Environment.CurrentDirectory;
    string sourceFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{sourceFileName}.{sourceFormat}";
    string targetFilePath = $"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName}\\{targetFileName}.{targetFormat}";

    Console.Clear();
    Console.WriteLine("Source: " + sourceFilePath);
    Console.WriteLine("Target: " + targetFilePath);

    FileFormatConverter converter = new();

    converter.AddFormat(new XmlFileFormat());
    converter.AddFormat(new BinaryFileFormat());

    try
    {
        converter.Convert(sourceFilePath, targetFilePath, sourceFormat, targetFormat);
        Console.WriteLine("Conversion successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Conversion failed: {ex.Message}");
    }
}


static void PressAnyKeyToBack()
{
    Console.WriteLine();
    Console.WriteLine("***********");
    Console.WriteLine("Enter any key to Back");
    Console.ReadLine();
}

static string GetDataType(string file)
{
    string[] options = { "xml", "bin" };
    int selectedIndex = 0;

    Console.CursorVisible = false;

    while (true)
    {
        // Display options
        Console.Clear();
        Console.WriteLine($"{file} type");
        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("-> " + options[i]);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("   " + options[i]);
            }
        }

        // Handle user input
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
        }
        else if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            selectedIndex = (selectedIndex + 1) % options.Length;
        }
        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            Console.WriteLine("You selected: " + options[selectedIndex]);

            break;
        }
    }

    return options[selectedIndex];
}

static void DisplayRecords(List<Record> records)
{
    Console.WriteLine("Records");
    foreach (var record in records)
    {
        Console.WriteLine("--------------------");
        Console.WriteLine($"Date: {record.Date}");
        Console.WriteLine($"Brand: {record.Brand}");
        Console.WriteLine($"Price: {record.Price:C}");
    }
}

static void DisplayRecord(Record record)
{
    Console.WriteLine("--------------------");
    Console.WriteLine($"Date: {record.Date}");
    Console.WriteLine($"Brand: {record.Brand}");
    Console.WriteLine($"Price: {record.Price:C}");
}