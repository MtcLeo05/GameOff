using System;

[Serializable]
public class CropData
{
    
    public CropBase.CropType cropType;
    public SerializableCropDrop[] drops;
    
    public int maxStage;
    public int stage;
    public float[] timeForStage;
    public float lifetime;

    public CropData(CropBase.CropType cropType, SerializableCropDrop[] drops, int maxStage, int stage, float lifetime, float[] timeForStage)
    {
        this.cropType = cropType;
        this.drops = drops;
        this.maxStage = maxStage;
        this.stage = stage;
        this.lifetime = lifetime;
        this.timeForStage = timeForStage;
    }
} 