using DotSpatial.Symbology;
using TerraExplorerX;

namespace _3D_Model_Accuracy_Checker;

internal class TerraExplorerManagement
{
    string path;
    string layerId;
    public string? currentPointNameClicked;
    SGWorld80 sGWorld = new();
    public Action<string, double, double, double> userClickedToCreatePoint;

    public TerraExplorerManagement(string path) {
        this.path = path;
        this.layerId = AddShapeFileToProjectTree(Path.GetFileName(path), path);
    }
    string AddShapeFileToProjectTree(string name, string path) {
        var f = sGWorld.Creator.CreateFeatureLayer(name, $"FileName = {path};TEPlugName = OGR;");
        //f.Refresh();
        //f.DataSourceInfo.Attributes.ImportAll = true;
        //f.Streaming = false;
        f.Position.AltitudeType = AltitudeTypeCode.ATC_TERRAIN_ABSOLUTE;
        f.Refresh();
        return f.ID;
    }
    public void RefresLayer()
    {
        sGWorld.ProjectTree.GetLayer(layerId).Refresh();
        sGWorld.Window.ShowMessageBarText("Layer Refreshed");
    }
    public void FlyTo(double x, double y, double z) => sGWorld.Navigate.JumpTo(sGWorld.Creator.CreatePosition(x,y,z,AltitudeTypeCode.ATC_TERRAIN_ABSOLUTE,Yaw:30, Pitch:300, Distance:10));   
    public void AttachEvents()
    {
        sGWorld.OnLButtonUp += OnLButtonUp;
    }
    private bool OnLButtonUp(int Flags, int X, int Y)
    {
        if (X == 0 && Y == 0)
            return true;
        sGWorld.OnLButtonUp -= OnLButtonUp;
        var position80 = sGWorld.Window.PixelToWorld(X,Y).Position;
        position80.ToAbsolute(AccuracyLevel.ACCURACY_FORCE_BEST_RENDERED);

        userClickedToCreatePoint.Invoke(currentPointNameClicked ,position80.X, position80.Y, position80.Altitude);
        return true;
    }
}
