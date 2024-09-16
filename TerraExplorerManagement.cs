using DotSpatial.Symbology;
using TerraExplorerX;

namespace _3D_Model_Accuracy_Checker;

internal class TerraExplorerManagement
{
    string path;
    string layerId;
    string modelId;
    public string? currentPointNameClicked;
    SGWorld80 sGWorld = new();
    public Action<string, double, double, double> userClickedToCreatePoint;
    private List<string> modelNames  = new();


    public TerraExplorerManagement(string path) {
        this.path = path;
        this.layerId = AddShapeFileToProjectTree(Path.GetFileName(path), path);
    }
    string AddShapeFileToProjectTree(string name, string path) {
        var f = sGWorld.Creator.CreateFeatureLayer(name, $"FileName = {path};TEPlugName = OGR;");
        //f.Refresh();

        if (sGWorld.Version.Type == TEVersionType.TEVT_PRO || sGWorld.Version.Type == TEVersionType.TEVT_PLUS)
        {
            f.DataSourceInfo.Attributes.ImportAll = true;
            f.Streaming = false;
        }

        f.Position.AltitudeType = AltitudeTypeCode.ATC_TERRAIN_ABSOLUTE;
        f.Refresh();
        return f.ID;
    }
    public void RefresLayer()
    {
        sGWorld.ProjectTree.GetLayer(layerId).Refresh();
        //sGWorld.ProjectTree.GetLayer(layerId).Position.ChangeAltitudeType(AltitudeTypeCode.ATC_TERRAIN_ABSOLUTE);
        sGWorld.Window.ShowMessageBarText("Layer Refreshed");
    }
    public void FlyTo(double x, double y, double z) => sGWorld.Navigate.JumpTo(sGWorld.Creator.CreatePosition(x,y,z,AltitudeTypeCode.ATC_TERRAIN_ABSOLUTE,Yaw:30, Pitch:300, Distance:10));   
    public void AttachEvents()
    {
        sGWorld.OnLButtonUp += OnLButtonUp;
        sGWorld.Command.Execute(1023);
    }
    private bool OnLButtonUp(int Flags, int X, int Y)
    {
        if (X == 0 && Y == 0)
            return true;
        sGWorld.OnLButtonUp -= OnLButtonUp;
        var position80 = sGWorld.Window.PixelToWorld(X,Y);
        modelId = position80.ObjectID;
        addNamesModelToList(sGWorld.ProjectTree.GetItemName(position80.ObjectID));
        position80.Position.ToAbsolute(AccuracyLevel.ACCURACY_FORCE_BEST_RENDERED);

        userClickedToCreatePoint.Invoke(currentPointNameClicked , position80.Position.X, position80.Position.Y, position80.Position.Altitude);
        sGWorld.Command.Execute(1023);
        return true;
    }
    private void addNamesModelToList(string name)
    {
        if (string.IsNullOrEmpty(name))
            return;
        if (modelNames.Contains(name))
            return;

        modelNames.Add(name);
    }
    public string GetNamesOfModels()
    {
        return string.Join("\n", modelNames);
    }
    public string GetSanpShot()
    {
        sGWorld.Command.Execute(1054);
        sGWorld.Navigate.JumpTo(modelId);
        Task.Delay(500);
        var photo =  sGWorld.Window.GetSnapShot(true);
        sGWorld.Command.Execute(1052);
        return photo;
    }

}
