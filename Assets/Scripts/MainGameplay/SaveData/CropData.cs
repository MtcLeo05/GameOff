using System;

[Serializable]
public class CropData
{
    public CropType cropType;
    
    public int stage;
    public float lifetime;

    public CropData(CropType cropType, int stage, float lifetime)
    {
        this.cropType = cropType;
        this.stage = stage;
        this.lifetime = lifetime;
    }
    
} 

[Serializable]
public enum CropType
{
    Tomato
}