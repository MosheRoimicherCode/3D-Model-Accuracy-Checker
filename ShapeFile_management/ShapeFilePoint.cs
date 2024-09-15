using Microsoft.VisualBasic.FileIO;
using DotSpatial.Data;
using DotSpatial.Topology;
using FeatureType = DotSpatial.Data.FeatureType;
using DotSpatial.Projections;
using DotSpatial.Projections.Transforms;
using System.Diagnostics.Metrics;
namespace ShapeFile_management2;

public class ShapeFilePoint
{
    public string path;
    public MyFeatureSet myFeatureSet;
    public Action pointAdded;
    public ShapeFilePoint(string path)
    {
        this.path = path;

        myFeatureSet = new MyFeatureSet(FeatureType.Point);
        myFeatureSet.CoordinateType = CoordinateType.Z;

        string wkt = @"PROJCS[""Israel / Israeli TM Grid"",
            GEOGCS[""Israel"",
                DATUM[""Israel"",
                    SPHEROID[""GRS 1980"",6378137,298.257222101,
                        AUTHORITY[""EPSG"",""7019""]],
                    TOWGS84[-48,55,52,0,0,0,0],
                    AUTHORITY[""EPSG"",""6141""]],
                PRIMEM[""Greenwich"",0,
                    AUTHORITY[""EPSG"",""8901""]],
                UNIT[""degree"",0.0174532925199433,
                    AUTHORITY[""EPSG"",""9122""]],
                AUTHORITY[""EPSG"",""4141""]],
            PROJECTION[""Transverse_Mercator""],
            PARAMETER[""latitude_of_origin"",31.7343936111111],
            PARAMETER[""central_meridian"",35.2045169444444],
            PARAMETER[""scale_factor"",1.0000067],
            PARAMETER[""false_easting"",219529.584],
            PARAMETER[""false_northing"",626907.39],
            UNIT[""metre"",1,
                AUTHORITY[""EPSG"",""9001""]],
            AXIS[""Easting"",EAST],
            AXIS[""Northing"",NORTH],
            AUTHORITY[""EPSG"",""2039""]]";


        // Create a ProjectionInfo object from the WKT string
        ProjectionInfo targetProjection = ProjectionInfo.FromEsriString(wkt);
        myFeatureSet.Projection = targetProjection;

        CreateNewPointShapefile();
    }
    void CreateNewPointShapefile() {

        // Create a new shapefile with Point geometry type
        // Define attribute columns for the shapefile (optional)
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
    public void AddPoint(string name, double x, double y, double z, Dictionary<string, string> data = null)
    {
        //myFeatureSet.FeatureAdded += OnFeatureAdded;
        NetTopologySuite.Geometries.Point point = new NetTopologySuite.Geometries.Point(x,y,z);

        // Create new point features and add them to the shapefile
        Feature featureToDelete = FindFeatureByAttribute(myFeatureSet, "Name", name);
        if (featureToDelete != null)
        {
            myFeatureSet.Features.Remove(featureToDelete);
            //DeleteFeatureByFID(myFeatureSet, featureToDelete.Fid);
            //UpdateFeatureGeometry(myFeatureSet, featureToDelete.Fid, point);
        }
        IFeature feature = myFeatureSet.AddFeature(point);

        if (data == null)
        {
            data = new Dictionary<string, string>();
        }

        // Add or update data values
        data["Name"] = name;
        data["X_measure"] = x.ToString();
        data["X_measure"] = y.ToString();
        data["X_measure"] = z.ToString();

        myFeatureSet.Save(); // Save changes to the shapefile
        

        AddAtributtesValue(feature.Fid, data);

    }

    private void OnFeatureAdded(object? sender, FeatureEventArgs e)
    {
        //pointAdded.Invoke();
    }

    public void DeleteFeatureByFID(FeatureSet featureSet, int fid)
    {
        // Ensure the FeatureSet is not null and has features
        if (featureSet == null || featureSet.Features.Count == 0)
        {
            throw new ArgumentException("FeatureSet is empty or null.");
        }

        // Find the feature by its FID
        IFeature featureToDelete = featureSet.Features.FirstOrDefault(f => f.Fid == fid);

        if (featureToDelete != null)
        {
            // Remove the feature from the FeatureSet
            featureSet.Features.Remove(featureToDelete);

            // Save the updated FeatureSet to the shapefile
            //featureSet.Save();

        }
        else
        {
            throw new Exception($"No feature found with FID {fid}.");
        }
    }
    
    public void UpdateFeatureGeometry(FeatureSet featureSet, int fid, NetTopologySuite.Geometries.Geometry newGeometry)
    {
        // Ensure the FeatureSet is not null and has features
        if (featureSet == null || featureSet.Features.Count == 0)
        {
            throw new ArgumentException("FeatureSet is empty or null.");
        }

        // Find the feature by its FID
        IFeature featureToUpdate = featureSet.Features.FirstOrDefault(f => f.Fid == fid);

        if (featureToUpdate != null)
        {
            // Update the geometry of the feature
            featureToUpdate.Geometry = newGeometry;

            // Save the updated FeatureSet to the shapefile
            featureSet.Save();

            Console.WriteLine($"Feature with FID {fid} has been updated with new geometry.");
        }
        else
        {
            Console.WriteLine($"No feature found with FID {fid}.");
        }
    }
    public Feature FindFeatureByAttribute(FeatureSet featureSet, string attributeName, object value)
    {
        // Ensure the FeatureSet is not null and the attribute exists
        if (featureSet == null || !featureSet.DataTable.Columns.Contains(attributeName))
            return null;

        // Loop through all features in the FeatureSet
        foreach (IFeature feature in featureSet.Features)
        {
            // Get the attribute value of the current feature for the given attribute name
            var featureValue = feature.DataRow[attributeName];

            // Compare the feature value to the given value
            if (featureValue != null && featureValue.Equals(value))
            {
                // Return the matching feature
                return feature as Feature;
            }
        }

        // Return null if no feature with the matching attribute value is found
        return null;
    }
    public void AddAtributtesValue(int fid, Dictionary<string, string> data)
    {

            var feature = myFeatureSet.GetFeatureByObjId(fid);

            foreach (var d in data)
            {
                if (myFeatureSet.DataTable.Columns.Contains(d.Key))
                {
                    feature.DataRow[d.Key] = d.Value;
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
