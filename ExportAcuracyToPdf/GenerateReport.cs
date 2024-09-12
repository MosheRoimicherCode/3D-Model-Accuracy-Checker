using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Globalization;
using System.Resources;
using Color = QuestPDF.Infrastructure.Color; // For File handling
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Text;
using IContainer = QuestPDF.Infrastructure.IContainer;
using QuestPDF.Previewer;
//using QuestPDF.Companion;

namespace ExportAcuracyToPdf;

public static class GenerateReport
{
    //ResourceManager _rm = new ResourceManager("GeneratePdf.Languages.Resources", typeof(Program).Assembly);
    private static float spacing = 18;
    static DocumentMetadata metaData = new();
    private static void GenerateFakeDataReport()
    {


        // Create lists to simulate your data categories
        List<List<string>> points = new();
        List<List<string>> lines = new();
        List<List<string>> polys = new();

        // Adding fake data for points
        points.Add(new List<string>() { "point a", "207898.13", "594880.19", "15.15", "point a", "207898.13", "594880.19", "15.15", "10","10" });
        points.Add(new List<string>() { "point a", "207898.13", "594880.19", "15.15", "point a", "207898.13", "594880.19", "15.15", "10", "10" });
        points.Add(new List<string>() { "point a", "207898.13", "594880.19", "15.15", "point a", "207898.13", "594880.19", "15.15", "10", "10" });

        
        // Generate the report PDF
        //ReportToPdf(points, "דומגמה של שם אתר", "he", "projectName");
    }

    public static void ReportToPdf(List<List<string>> points, List<string> accuracy,  string siteName, string projectName, string pdfPath)
    {
        metaData.CreationDate = DateTimeOffset.Now.Date;
        metaData.Creator = "Automatic Report From Terra Explorer";
        metaData.Producer = "Kav Medida - Israel";
        metaData.Title = projectName;

        //DefineCulture(language);
        QuestPDF.Settings.License = LicenseType.Community;

        // Get the base directory of the project
        string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logoKav.png");

        QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Column(column =>
                    {
                        if (File.Exists(logoPath))
                        {
                            column.Item().Height(5).Background("4083c4");
                            column.Item().AlignCenter().Width(100).Image(logoPath);
                            column.Item().Height(5).Background("4083c4");
                        }
                        column.Spacing(10);
                        column.Item().Background("#00FF00");

                        column.Item().Text(projectName + " - " + siteName).AlignCenter().FontSize(8);
                        column.Item().Text("Generated at" + " " + DateTime.Now.ToShortDateString()).AlignCenter().FontSize(8);

                        column.Item().Height(1).Background(Colors.Black);

                    });

                page.Content()
                    .Column(col =>
                    {
                        // Table 1: Points Information
                        col.Item().Border(1).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // "Name"
                                columns.RelativeColumn(); // "X_original"
                                columns.RelativeColumn(); // "Y_original"
                                columns.RelativeColumn(); // "Z_original"
                                columns.RelativeColumn(); // "X_measure"
                                columns.RelativeColumn(); // "Y_measure"
                                columns.RelativeColumn(); // "Z_measure"
                                columns.RelativeColumn(); // "X_offset"
                                columns.RelativeColumn(); // "Y_offset"
                                columns.RelativeColumn(); // "Z_offset"
                                columns.RelativeColumn(); // "X_offset_deviation"
                                columns.RelativeColumn(); // "Y_offset_deviation"
                                columns.RelativeColumn(); // "Z_offset_deviation"
                            });

                            // Table 1: Headers
                            table.Cell().Element(TableHeader).Text("Name").FontSize(8);
                            table.Cell().Element(TableHeader).Text("X_original").FontSize(8);
                            table.Cell().Element(TableHeader).Text("Y_original").FontSize(8);
                            table.Cell().Element(TableHeader).Text("Z_original").FontSize(8);
                            table.Cell().Element(TableHeader).Text("X_measure").FontSize(8);
                            table.Cell().Element(TableHeader).Text("Y_measure").FontSize(8);
                            table.Cell().Element(TableHeader).Text("Z_measure").FontSize(8);
                            table.Cell().Element(TableHeader).Text("X_offset").FontSize(8);
                            table.Cell().Element(TableHeader).Text("Y_offset").FontSize(8);
                            table.Cell().Element(TableHeader).Text("Z_offset").FontSize(8);
                            table.Cell().Element(TableHeader).Text("X_offset_deviation").FontSize(8);
                            table.Cell().Element(TableHeader).Text("Y_offset_deviation").FontSize(8);
                            table.Cell().Element(TableHeader).Text("Z_offset_deviation").FontSize(8);

                            // Table 1: Rows (data from points list)
                            foreach (var list in points)
                            {
                                foreach (var item in list)
                                {
                                    table.Cell().Element(Block).Text(item).FontSize(8); // Display data in each cell
                                }
                            }
                        });

                        // Spacing between the two tables
                        col.Item().PaddingVertical(1, Unit.Centimetre);

                        // Table 2: Accuracy Deviation
                        col.Item().Border(1).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // "X Deviation"
                                columns.RelativeColumn(); // "Y Deviation"
                                columns.RelativeColumn(); // "Z Deviation"
                                

                            });

                            // Table 2: Headers
                            table.Cell().Element(TableHeader).Text("X Deviation").FontSize(8);
                            table.Cell().Element(TableHeader).Text("Y Deviation").FontSize(8);
                            table.Cell().Element(TableHeader).Text("Z Deviation").FontSize(8);

                            // Table 2: Rows (data from accuracy list)
                            for (var i = 0; i < 3; i++)
                            {
                                table.Cell().Element(Block).Text(accuracy[i]).FontSize(12); // Display data in each cell
                            }
                        });

                        // Spacing between the two tables
                        col.Item().PaddingVertical(1, Unit.Centimetre);

                        // Table 2: Accuracy Deviation
                        col.Item().Border(1).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // "Accuracy Scale"
                            });

                            table.Cell().Element(TableHeader).Text("Accuracy Scale").FontSize(20);
                            table.Cell().Element(Block).Text(accuracy[3]).FontSize(20); // Display data in each cell
                            
                        });
                    });



                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page");
                        x.CurrentPageNumber();
                    });
            });
        }).GeneratePdfAndShow();
        
    }

    //.WithMetadata(metaData).GeneratePdf(pdfPath);

    static QuestPDF.Infrastructure.IContainer Block(IContainer container)
    {
        return container
            .Border(1)
            .ShowOnce()
            .MinWidth(50)
            .MinHeight(10)
            .AlignCenter()
            .AlignMiddle();
    }

    static QuestPDF.Infrastructure.IContainer TableHeader(IContainer container)
    {
        return container
            .Border(1)
            .Background("659ed2")
            .ShowOnce()
            .MinWidth(50)
            .MinHeight(15)
            .AlignCenter()
            .AlignMiddle()
            ;
    }


    private static void DefineCulture(string lng = "he-HE")
    {
        string cultureName = lng.Length > 0 ? lng : "en-US"; // Default to English if no argument
        CultureInfo culture = new CultureInfo(cultureName);
        // Set current UI culture
        CultureInfo.CurrentUICulture = culture;
    }
}
