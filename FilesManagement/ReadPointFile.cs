using System.Globalization;

namespace FilesManagement;

public class PointData
{
    public string Name { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, X: {X}, Y: {Y}, Z: {Z}";
    }
}
static public class ReadPointFile
{
    static public List<PointData> ReadFile(string filePath)
    {
        var points = new List<PointData>();
        string[] separators = null;

        using (var reader = new StreamReader(filePath))
        {
            // Detect the separator from the first line
            string firstLine = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(firstLine))
            {
                throw new Exception("File is empty or invalid.");
            }

            // Detect separator on the first line
            if (firstLine.Contains(","))
            {
                separators = new[] { "," };
            }
            else if (firstLine.Contains("\t"))
            {
                separators = new[] { "\t" };
            }
            else
            {
                throw new Exception("Unknown file format. Only comma or tab separated files are supported.");
            }

            // Process the first line
            ProcessLine(firstLine, separators, points);

            // Process the remaining lines
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;  // Skip empty lines
                }
                ProcessLine(line, separators, points);
            }
        }

        return points;
    }

    private static void ProcessLine(string line, string[] separators, List<PointData> points)
    {
        var values = line.Split(separators, StringSplitOptions.None);

        if (values.Length == 4)
        {
            try
            {
                var point = new PointData
                {
                    Name = values[0],
                    X = double.Parse(values[1], CultureInfo.InvariantCulture),
                    Y = double.Parse(values[2], CultureInfo.InvariantCulture),
                    Z = double.Parse(values[3], CultureInfo.InvariantCulture)
                };

                points.Add(point);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error parsing line: {line}. {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Invalid number of values in line: {line}");
        }
    }

    }
