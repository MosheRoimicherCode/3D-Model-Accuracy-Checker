using TerraExplorerX;

namespace _3D_Model_Accuracy_Checker;

internal class TerraExplorerManagement
{
    string path;
    string layerId;
    SGWorld80 sGWorld = new();
    public Action<double, double, double> userClickedToCreatePoint;
    public TerraExplorerManagement(string path) {
        this.path = path;
        this.layerId = AddShapeFileToProjectTree(Path.GetFileName(path), path);
    }
    string AddShapeFileToProjectTree(string name, string path) {
        var f = sGWorld.Creator.CreateFeatureLayer(name, $"FileName = {path};TEPlugName = OGR;");
        f.DataSourceInfo.Attributes.ImportAll = true;
        f.Position.AltitudeType = AltitudeTypeCode.ATC_TERRAIN_ABSOLUTE;
        f.Refresh();
        return f.ID;
    }
    public void RefresLayer() => sGWorld.ProjectTree.GetLayer(layerId).Refresh();
    public void FlyTo(double x, double y, double z) => sGWorld.Navigate.FlyTo(sGWorld.Creator.CreatePosition(x,y,z,AltitudeTypeCode.ATC_TERRAIN_ABSOLUTE,Yaw:30, Pitch:300, Distance:10));   
    public void AttachEvents()
    {
        sGWorld.OnLButtonUp += OnLButtonUp;
    }
    private bool OnLButtonUp(int Flags, int X, int Y)
    {
        sGWorld.OnLButtonUp -= OnLButtonUp;
        if (X == 0 && Y == 0)
            return true;
        var position80 = sGWorld.Window.PixelToWorld(X,Y).Position;

        // userClickedToCreatePoint.Invoke(position80.X, position80.Y, position80.Altitude);


        sGWorld.ProjectTree.GetLayer(layerId).FeatureGroups.Point.CreateFeature(sGWorld.Creator.GeometryCreator.CreatePointGeometry(position80));
        sGWorld.ProjectTree.GetLayer(layerId).Save();
        sGWorld.ProjectTree.GetLayer(layerId).Refresh();
        return true;
    }
}
