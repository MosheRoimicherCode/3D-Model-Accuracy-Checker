using DotSpatial.Data;
using ExportAcuracyToPdf;
using FilesManagement;
using ShapeFile_management2;
using System.Windows.Forms;
using System.Xml.Linq;
using VerifyTests;
namespace _3D_Model_Accuracy_Checker;

public partial class Main : Form
{
    TerraExplorerManagement terraExplorerManagement;
    ShapeFile_management2.ShapeFilePoint shapefile;

    string currentPointNameFromGridVew = "";

    public Main()
    {
        InitializeComponent();
        //calculateTest();
    }

    private void LoadFile_btn_Click(object sender, EventArgs e)
    {
        try
        {
            // Load and filter data
            var points = ReadPointFile.ReadFile(ChooseFile());
            List<PointData> filteredPoints;
            // Show the input dialog
            using (var inputDialog = new InputDialog())
            {
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the filter text from the dialog
                    string filterText = inputDialog.InputText;

                    // Split the input text based on common delimiters
                    var delimiters = new[] { "\r\n", "\n", ",", " ", "\t" };
                    var filters = filterText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);


                    filteredPoints = points
                        .Where(point => filters.Contains(point.Name))
                        .ToList();


                    if (filteredPoints.Count <= 0)
                    {
                        filteredPoints = points.ToList();
                    }
                }
                else
                {
                    filteredPoints = points.ToList();
                }

                List<string> list = new List<string>()
                {
                    "OriginalX",
                    "OriginalY",
                    "OriginalZ"
                };

                PopulateGridView(filteredPoints, list);
            }
            
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while generating the report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void PopulateGridView(List<PointData> points, List<string> colmnNames)
    {
        try
        {
            foreach (var point in points)
            {
                DataGridViewRow row = (DataGridViewRow)points_dataGridView.Rows[0].Clone();
                int nameColumnIndex = points_dataGridView.Columns["ID"].Index;
                int deleteColumnIndex = points_dataGridView.Columns["Delete"].Index;

                int X = points_dataGridView.Columns[colmnNames[0]].Index;
                int Y = points_dataGridView.Columns[colmnNames[1]].Index;
                int Z = points_dataGridView.Columns[colmnNames[2]].Index;


                // Set values for the X, Y, and Z columns
                row.Cells[X].Value = point.X;
                row.Cells[Y].Value = point.Y;
                row.Cells[Z].Value = point.Z;

                // Create a button cell for the "ID" column and set the button text to point.Name
                DataGridViewButtonCell buttonCell = new DataGridViewButtonCell();
                buttonCell.Value = point.Name;  // Set the button text to the point.Name
                row.Cells[nameColumnIndex] = buttonCell;
                buttonCell.UseColumnTextForButtonValue = true;

                //REMOVE CELL BUTTON
                DataGridViewButtonCell buttonCelldelete = new DataGridViewButtonCell();
                buttonCelldelete.Value = "Delete Point";  // Set the button text to the point.Name
                row.Cells[deleteColumnIndex] = buttonCelldelete;
                row.Cells[deleteColumnIndex].Style.BackColor = Color.Red;
                row.Cells[deleteColumnIndex].Style.ForeColor = Color.Red;
                row.Cells[deleteColumnIndex].Style.ForeColor = Color.White;
                buttonCelldelete.UseColumnTextForButtonValue = true;

                points_dataGridView.Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while writing to cell: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private static string GetShapefilePath()
    {
        try
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Shapefile";
                saveFileDialog.Filter = "Shapefiles (*.shp)|*.shp";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return saveFileDialog.FileName;
                }
                else
                {
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while generating the report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return null;
    }
    private static string ChooseFile()
    {
        try
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV, PNT, and TXT files (*.csv, *.pnt, *.txt)|*.csv;*.pnt;*.txt|All files (*.*)|*.*";

            openFileDialog.FilterIndex = 1;  // Index of the filter that is selected by default

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;  // Return the selected file's path
            }
            else
            {
                return null;  // Return null if no file is selected or dialog is canceled
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    private void points_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex >= 0 && e.RowIndex != (sender as DataGridView).RowCount - 1) // Ensures it's not a header row
            {
                DataGridViewRow row = points_dataGridView.Rows[e.RowIndex];
                if (e.ColumnIndex == 0)
                {
                    // Access row data
                    // Get the value of the point name, check for null, and assign to currentPointNameFromGridView
                    string currentPointNameFromGridVew = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
                    terraExplorerManagement.currentPointNameClicked = currentPointNameFromGridVew;

                    // Get X, Y, Z values, check for null, and convert to string (or set default value if null)
                    string xString = row.Cells[2].Value != null ? row.Cells[2].Value.ToString() : string.Empty;
                    string yString = row.Cells[3].Value != null ? row.Cells[3].Value.ToString() : string.Empty;
                    string zString = row.Cells[4].Value != null ? row.Cells[4].Value.ToString() : string.Empty;

                    double x, y, z;


                    if (!double.TryParse(xString, out x))
                        MessageBox.Show($"Invalid value for X at {currentPointNameFromGridVew} point");
                    if (!double.TryParse(yString, out y))
                        MessageBox.Show($"Invalid value for Y at {currentPointNameFromGridVew} point");
                    if (!double.TryParse(zString, out z))
                        MessageBox.Show($"Invalid value for Z at {currentPointNameFromGridVew} point");

                    terraExplorerManagement.AttachEvents();
                    terraExplorerManagement.FlyTo(x, y, z);


                }
                else if (e.ColumnIndex == 1)
                {
                    points_dataGridView.Rows.Remove(row);
                }
                else
                {
                    //throw new Exception("Cannot Handeld the event |||| ColumnIndex != 0");
                }
            }
            else
            {
                //throw new Exception("Cannot Handeld the event");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Execption When Clicking on cell: {ex.Message}");
        }
    }
    private void shapefile_btn_Click(object sender, EventArgs e)
    {
        try
        {
            // Prompt user to select a shapefile path
            string path = GetShapefilePath();

            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("No shapefile path was provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Initialize the shapefile
            shapefile = new ShapeFilePoint(path);
            shapefile.pointAdded += RefreshTerraLayer;
            terraExplorerManagement = new(path);
            terraExplorerManagement.userClickedToCreatePoint += userClickedToCreatePoint;
            terraExplorerManagement.currentPointNameClicked = currentPointNameFromGridVew;

            // Enable buttons if shapefile is loaded successfully
            LoadFile_btn.Enabled = true;
            Calc_btn.Enabled = true;
        }
        catch (Exception ex)
        {
            // Display the error message
            MessageBox.Show($"An error occurred while creating the shapefile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    double xOff, yOff, zOff;
    List<double> offsets;
    List<string> columnnames;
    Dictionary<string, string> map;
    private void userClickedToCreatePoint(string? name, double x, double y, double z)
    {
        try
        {
            terraExplorerManagement.currentPointNameClicked = name;
            map = new Dictionary<string, string>();

            columnnames = new List<string>() { "MeasureX", "MeasureY", "MeasureZ" };
            var poin = new PointData(name, x, y, z);
            insertDataToCells_gridView(poin, columnnames);


            map["X_measure"] = x.ToString();
            map["Y_measure"] = y.ToString();
            map["Z_measure"] = z.ToString();

            columnnames = new List<string>() { "OriginalX", "OriginalY", "OriginalZ" };


            var offsetList = GetDataFromCells_gridView(name, columnnames, points_dataGridView);
            if (!double.TryParse(offsetList[0], out xOff))
                MessageBox.Show($"Invalid value for X at {currentPointNameFromGridVew} point");
            if (!double.TryParse(offsetList[1], out yOff))
                MessageBox.Show($"Invalid value for Y at {currentPointNameFromGridVew} point");
            if (!double.TryParse(offsetList[2], out zOff))
                MessageBox.Show($"Invalid value for Z at {currentPointNameFromGridVew} point");

            map["X_original"] = xOff.ToString();
            map["Y_original"] = yOff.ToString();
            map["Z_original"] = zOff.ToString();


            offsets = new()
            {
               (xOff - x),
               (yOff - y),
               (zOff - z)
            };

            columnnames = new List<string>() { "OffsetX", "OffsetY", "OffsetZ" };
            poin = new PointData(name, offsets[0], offsets[1], offsets[2]);
            insertDataToCells_gridView(poin, columnnames);

            map["X_offset"] = offsets[0].ToString();
            map["Y_offset"] = offsets[1].ToString();
            map["Z_offset"] = offsets[2].ToString();

            shapefile.AddPoint(name, x, y, z, map);
            // Save changes to the shapefile
            shapefile.myFeatureSet.Save();
            RefreshTerraLayer();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred whilecreating point: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void insertDataToCells_gridView(PointData point, List<string> columnnames)
    {
        try
        {
            var indx = FindRowById(points_dataGridView, point.Name, "ID");
            List<double> data = new List<double>() { point.X, point.Y, point.Z };
            EditRow(points_dataGridView, indx, columnnames, data);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred whilewriting to cell: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private List<string> GetDataFromCells_gridView(string name, List<string> columnnames, DataGridView dataGridView)
    {
        try
        {
            List<string> values = new();
            // Loop through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (var columnName in columnnames)
                {
                    // Check if the column "ID" exists and if the row is not a new row
                    if (row.Index == FindRowById(dataGridView, name, "ID"))
                    {
                        values.Add(row.Cells[columnName].Value != null ? row.Cells[columnName].Value.ToString() : string.Empty);
                    }
                }
            }
            // If no match is found, return -1
            return values;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while reading cell: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return new List<string>();
        
    }
    private int FindRowById(DataGridView dataGridView, string idValue, string columnName)
    {
        try
        {
            // Loop through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Check if the column "ID" exists and if the row is not a new row
                if (row.Cells[columnName] != null && !row.IsNewRow)
                {
                    // Compare the value in the "ID" column to the specified idValue
                    if (row.Cells[columnName].Value != null && row.Cells[columnName].Value.ToString() == idValue)
                    {
                        // If a match is found, return the row index
                        return row.Index;
                    }
                }
            }

            // If no match is found, return -1
            return -1;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while searching row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // If no match is found, return -1
        return -1;
    }
    public void EditRow(DataGridView dataGridView, int rowIndex, List<string> columnNames, List<double> values)
    {
        try
        {
            // Check if the rowIndex is valid
            if (rowIndex < 0 || rowIndex >= dataGridView.Rows.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid row index.");
            }

            // Check if columnNames and values have the same length
            if (columnNames.Count != values.Count)
            {
                throw new ArgumentException("Column names and values must have the same number of elements.");
            }

            // Get the row to be edited
            DataGridViewRow row = dataGridView.Rows[rowIndex];

            // Loop through the column names and update the corresponding cells
            for (int i = 0; i < columnNames.Count; i++)
            {
                string columnName = columnNames[i];

                // Check if the column exists
                if (dataGridView.Columns[columnName] != null)
                {
                    // Set the cell value for the specified column in the row
                    row.Cells[columnName].Value = values[i];
                }
                else
                {
                    throw new ArgumentException($"Column '{columnName}' does not exist in the DataGridView.");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while editing row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    public void RefreshTerraLayer()
    {
        try
        {
            terraExplorerManagement.RefresLayer();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while reloading the layer : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }



    double Xaccuracy;
    double Yaccuracy;
    double Zaccuracy;

    private void Calc_btn_Click(object sender, EventArgs e)
    {
        try
        {
            List<double> offetsX = new List<double>();
            List<double> offetsY = new List<double>();
            List<double> offetsZ = new List<double>();

            double x, y, z;
            DataGridView datagrid = points_dataGridView;

            if (datagrid.Rows.Count <= 3)
            {
                throw new Exception("Insufficient control points. The minimum required is 3. \n  *** You can try to seach for old maps. ***");
            }

            foreach (DataGridViewRow row in datagrid.Rows)
            {
                // Skip the empty/new row
                if (row.IsNewRow)
                {
                    continue;
                }

                double.TryParse((row.Cells["OffsetX"].Value.ToString()), out x);
                offetsX.Add(x);
                double.TryParse((row.Cells["OffsetY"].Value.ToString()), out y);
                offetsY.Add(y);
                double.TryParse((row.Cells["OffsetZ"].Value.ToString()), out z);
                offetsZ.Add(z);
            }

            double averegeX = offetsX.Average();
            double averegeY = offetsY.Average();
            double averegeZ = offetsZ.Average();

            offetsX.Clear();
            offetsY.Clear();
            offetsZ.Clear();


            List<string> columnnames = new List<string>() { "OffMinusAverage_X", "OffMinusAverage_Y", "OffMinusAverage_Z" };
            foreach (DataGridViewRow row in datagrid.Rows)
            {
                // Skip the empty/new row
                if (row.IsNewRow)
                {
                    continue;
                }

                double.TryParse((row.Cells["OffsetX"].Value.ToString()), out x);
                offetsX.Add(Math.Abs(x - averegeX) * Math.Abs(x - averegeX));
                double.TryParse((row.Cells["OffsetY"].Value.ToString()), out y);
                offetsY.Add(Math.Abs(y - averegeY) * Math.Abs(y - averegeY));
                double.TryParse((row.Cells["OffsetZ"].Value.ToString()), out z);
                offetsZ.Add(Math.Abs(z - averegeZ) * Math.Abs(z - averegeZ));

                var poin = new PointData(row.Cells["ID"].Value?.ToString(), (x - averegeX), (y - averegeY), (z - averegeZ));
                insertDataToCells_gridView(poin, columnnames);
            }



            Xaccuracy = Math.Sqrt(offetsX.Average());
            Yaccuracy = Math.Sqrt(offetsY.Average());
            Zaccuracy = Math.Sqrt(offetsY.Average());

            VerifyAccuracy();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while calculation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    void calculateTest()
    {
        List<double> offetsX = new List<double>(){
            0.019999999989522600,
            0.000000000000000000,
            0.010000000009313200,
            0.009999999980209400,
            0.010000000009313200,
            0.010000000009313200,

        };
        List<double> offetsY = new List<double>(){
            0.050000000046566100,
            0.000000000000000000,
            0.000000000000000000,
            -0.010000000009313200,
            0.020000000018626500,
            0.000000000000000000

        };
        List<double> offetsZ = new List<double>(){
            0.139999999999993000,
            0.000000000000000000,
            0.000000000000000000,
            0.000000000000000000,
            0.010000000000005100,
            0.020000000000003100,
        };



        double averegeX = offetsX.Average();
        double averegeY = offetsY.Average();
        double averegeZ = offetsZ.Average();

        offetsX.Clear();
        offetsY.Clear();
        offetsZ.Clear();

        List<double> offetsX_2 = new List<double>(){
            (0.019999999989522600 - averegeX)*(0.019999999989522600 - averegeX),
            (0.000000000000000000 - averegeX)*(0.000000000000000000 - averegeX),
            (0.010000000009313200 - averegeX)*(0.010000000009313200 - averegeX),
            (0.009999999980209400 - averegeX)*(0.009999999980209400 - averegeX),
            (0.010000000009313200 - averegeX)*(0.010000000009313200 - averegeX),
            (0.010000000009313200 - averegeX)*(0.010000000009313200 - averegeX),

        };
        List<double> offetsY_2 = new List<double>(){
            (0.050000000046566100 - averegeY)*(0.050000000046566100 - averegeY),
            (0.000000000000000000- averegeY)*(0.000000000000000000- averegeY),
            (0.000000000000000000- averegeY)*(0.000000000000000000- averegeY),
            (-0.010000000009313200- averegeY)*(-0.010000000009313200- averegeY),
            (0.020000000018626500- averegeY)*(0.020000000018626500- averegeY),
            (0.000000000000000000- averegeY)*(0.000000000000000000- averegeY)

        };
        List<double> offetsZ_2 = new List<double>(){
            (0.139999999999993000 - averegeZ)*(0.139999999999993000 - averegeZ),
            (0.000000000000000000 - averegeZ)*(0.000000000000000000 - averegeZ),
            (0.000000000000000000 - averegeZ)*(0.000000000000000000 - averegeZ),
            (0.000000000000000000 - averegeZ)*(0.000000000000000000 - averegeZ),
            (0.010000000000005100 - averegeZ)*(0.010000000000005100 - averegeZ),
            (0.020000000000003100 - averegeZ)*(0.020000000000003100 - averegeZ)
        };



        Xaccuracy = Math.Sqrt(offetsX_2.Average());
        Yaccuracy = Math.Sqrt(offetsY_2.Average());
        Zaccuracy = Math.Sqrt(offetsZ_2.Average());

        VerifyAccuracy();
    }
    private void VerifyAccuracy()
    {
        var accuracyLevels = new (double X, double Y, double Z, string Scale)[]
        {
            (0.01, 0.01, 0.01, "1:50"),
            (0.03, 0.03, 0.02, "1:100"),
            (0.06, 0.06, 0.05, "1:250"),
            (0.13, 0.13, 0.1, "1:500"),
            (0.25, 0.25, 0.2, "1:1000"),
            (0.63, 0.63, 0.5, "1:2500"),
            (1.25, 1.25, 1, "1:5000"),
            (2.5, 2.5, 2, "1:10000"),
            (6.25, 6.25, 5, "1:25000"),
            (12.5, 12.5, 10, "1:50000")
        };

        foreach (var (x, y, z, scale) in accuracyLevels)
        {
            if (Xaccuracy <= x && Yaccuracy <= y && Zaccuracy <= z)
            {
                resultTxtBox.Text = scale;
                return;
            }
        }

        resultTxtBox.Text = "Accuracy out of range. The minimum acceptable accuracy is 1:50,000.";
    }
    private void Export_btn_Click(object sender, EventArgs e)
    {
        try
        {
            var list = new List<string>() { Xaccuracy.ToString(), Yaccuracy.ToString(), Zaccuracy.ToString(), resultTxtBox.Text };
            var imagePath = terraExplorerManagement.GetSanpShot();
            GenerateReport.ReportToPdf(GetDataGridViewData(points_dataGridView), list, terraExplorerManagement.GetNamesOfModels(), "", imagePath);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while generating the report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private List<List<string>> GetDataGridViewData(DataGridView dataGridView)
    {
        var result = new List<List<string>>();

        foreach (DataGridViewRow row in dataGridView.Rows)
        {
            if (!row.IsNewRow) // Skip the new input row
            {
                var rowData = new List<string>();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value?.ToString() == "Delete Point")
                        continue;

                    string cellValue = cell.Value?.ToString() ?? string.Empty;

                    // Try to parse the cell value as a double
                    if (double.TryParse(cellValue, out double doubleValue))
                    {
                        // Format parsed double to 2 decimal places
                        rowData.Add(doubleValue.ToString("0.00"));
                    }
                    else
                    {
                        // If not a valid double, just add the original string value
                        rowData.Add(cellValue);
                    }
                }

                result.Add(rowData);
            }
        }

        return result;
    }


}
