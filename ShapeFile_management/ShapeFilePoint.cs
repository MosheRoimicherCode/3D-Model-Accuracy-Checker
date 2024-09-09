using Microsoft.VisualBasic.FileIO;
using DotSpatial.Data;
using DotSpatial.Topology;
using FeatureType = DotSpatial.Data.FeatureType;
namespace ShapeFile_management2;

public class ShapeFilePoint
{
    string path;
    string type;
    MyFeatureSet myFeatureSet;
    ShapeFilePoint(string path)
    {
        this.path = path;

        myFeatureSet = new MyFeatureSet(DotSpatial.Data.FeatureType.Point);
        CheckPath(path);
        if (!File.Exists(path))
            CreateNewPointShapefile();
    }

    private void CreateNewPointShapefile() {

        // Create a new shapefile with Point geometry type
        // Define attribute columns for the shapefile (optional)
        myFeatureSet.DataTable.Columns.Add("ID", typeof(int));
        myFeatureSet.DataTable.Columns.Add("Name", typeof(string));

        myFeatureSet.DataTable.Columns.Add("X_original", typeof(string));
        myFeatureSet.DataTable.Columns.Add("Y_original", typeof(string));
        myFeatureSet.DataTable.Columns.Add("Z_original", typeof(string));

        myFeatureSet.DataTable.Columns.Add("X_measure", typeof(string));
        myFeatureSet.DataTable.Columns.Add("Y_measure", typeof(string));
        myFeatureSet.DataTable.Columns.Add("Z_measure", typeof(string));

        myFeatureSet.DataTable.Columns.Add("X_offset", typeof(string));
        myFeatureSet.DataTable.Columns.Add("Y_offset", typeof(string));
        myFeatureSet.DataTable.Columns.Add("Z_offset", typeof(string));

        // Save the shapefile to the specified path
        myFeatureSet.SaveAs(path, true);
    }

    public void AddPoint(double x, double y, double z, Dictionary<string, string> data)
    {
        NetTopologySuite.Geometries.Point point = new NetTopologySuite.Geometries.Point(x, y, z);

        // Create new point features and add them to the shapefile
        IFeature feature = myFeatureSet.AddFeature(point);

        AddAtributtesValue(feature.Fid, data);

        // Save changes to the shapefile
        myFeatureSet.Save();
    }

    public void AddAtributtesValue(int fid, Dictionary<string, string> data)
    {
        var feature = myFeatureSet.GetFeatureByObjId(fid);

        foreach (var d in data)
        {
            if (myFeatureSet.DataTable.Columns.Contains(d.Key))
            {
                feature.DataRow[d.Key] = data[d.Value];
            }
            else
            {
                myFeatureSet.DataTable.Columns.Add(d.Key, d.Value.GetType());
                feature.DataRow[d.Key] = data[d.Value];
            }
        }

        // Save changes to the shapefile
        myFeatureSet.Save();
    }
   
    public NetTopologySuite.Geometries.Coordinate GetCoordinateFromPointFeature(int ObjectId)
    {
        List<double> coordinates = new List<double>();
        var feature = myFeatureSet.GetFeatureByObjId(ObjectId);

        if (ObjectId == null)
            throw new Exception("Object id and Name id null request");

        return feature.Geometry.Coordinate.CoordinateValue;
    }
    
    private string CheckPath(string path)
    {
        string directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return directory;
    }
}
