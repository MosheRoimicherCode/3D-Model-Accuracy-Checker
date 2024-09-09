using System.Globalization;

namespace FilesManagement;

class PointData
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
    static List<PointData> ReadFile(string filePath)
    {
        var points = new List<PointData>();
        string[] separators = null;

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                // Read the first line (header) to detect the separator
                string headerLine = reader.ReadLine();
                if (headerLine.Contains(","))
                {
                    separators = new[] { "," };
                }
                else if (headerLine.Contains("\t"))
                {
                    separators = new[] { "\t" };
                }
                else
                {
                    throw new Exception("Unknown file format. Only comma or tab separated files are supported.");
                }

                // Read the rest of the file
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    var values = line.Split(separators, StringSplitOptions.None);

                    if (values.Length == 4)
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
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return points;
    }
}
