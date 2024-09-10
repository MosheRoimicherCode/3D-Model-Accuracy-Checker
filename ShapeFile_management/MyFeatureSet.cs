using DotSpatial.Data;

public class MyFeatureSet : DotSpatial.Data.FeatureSet
{
    public MyFeatureSet(DotSpatial.Data.FeatureType featureType) : base(featureType){}
    public MyFeatureSet(){}
    public MyFeatureSet(List<IFeature> features) : base(features){}
    
    // Method to retrieve a feature by its object ID
    public IFeature GetFeatureByObjId(int fid) => Features.Where(x => x.Fid == fid).First();      
}
