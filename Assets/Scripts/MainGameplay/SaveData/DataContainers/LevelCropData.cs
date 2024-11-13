using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelCropData
{
    public SerializableDictionary<string, CropData> crops;
    
    public LevelCropData()
    {
        crops = new SerializableDictionary<string, CropData>();
    }
}