using DotSpatial.Data;
using FilesManagement;
using System.Windows.Forms;
namespace _3D_Model_Accuracy_Checker;

public partial class Main : Form
{
    TerraExplorerManagement terraExplorerManagement; 
    ShapeFile_management2.ShapeFilePoint shapefile;

    public Main()
    {
        InitializeComponent();
    }

    private void LoadFile_btn_Click(object sender, EventArgs e)
    {
        var points = ReadPointFile.ReadFile(ChooseFile());
        PopulateGridView(points);

 

    }
    private void PopulateGridView(List<PointData> points)
    {
        foreach (var point in points)
        {
            DataGridViewRow row = (DataGridViewRow)points_dataGridView.Rows[0].Clone();
            int nameColumnIndex = points_dataGridView.Columns["ID"].Index;
            int X = points_dataGridView.Columns["OriginalX"].Index;
            int Y = points_dataGridView.Columns["OriginalY"].Index;
            int Z = points_dataGridView.Columns["OriginalZ"].Index;

            row.Cells[nameColumnIndex].Value = point.Name;
            row.Cells[X].Value = point.X;
            row.Cells[Y].Value = point.Y;
            row.Cells[Z].Value = point.Z;

            points_dataGridView.Rows.Add(row);
        }
    }

    private static string GetShapefilePath()
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
    private static string ChooseFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        openFileDialog.Filter = "CSV files (*.csv)|*.csv|PNT files (*.pnt)|*.pnt|Text files (*.txt)|*.txt|All files (*.*)|*.*";  // Filter for CSV, PNT, and TXT files
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

    private void points_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) // Ensures it's not a header row
        {
            DataGridViewRow row = points_dataGridView.Rows[e.RowIndex];
            if (e.ColumnIndex < 0)
            {
                // Access row data
                string name = row.Cells[0].Value.ToString();

                string xString = row.Cells[1].Value.ToString();
                string yString = row.Cells[2].Value.ToString();
                string zString = row.Cells[3].Value.ToString();
                double x, y, z;


                if (!double.TryParse(xString, out x))
                    MessageBox.Show($"Invalid value for X at {name} point");
                else if (!double.TryParse(yString, out y))
                    MessageBox.Show($"Invalid value for Y at {name} point");
                else if (!double.TryParse(zString, out z))
                    MessageBox.Show($"Invalid value for Z at {name} point");
                else
                {
                    terraExplorerManagement.FlyTo(x, y, z);
                    terraExplorerManagement.AttachEvents();
                }

            }
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
            shapefile = new(path);
            terraExplorerManagement = new(path);
            terraExplorerManagement.userClickedToCreatePoint += userClickedToCreatePoint;

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

    private void userClickedToCreatePoint(double x, double y, double z)
    {
        shapefile.AddPoint(x, y, z, null);
        // Save changes to the shapefile
        //shapefile.myFeatureSet.Save();
        //terraExplorerManagement.RefresLayer();
    }
}
