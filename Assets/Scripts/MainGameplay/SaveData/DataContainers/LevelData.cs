using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int dayCount;
    public float dayTimer;
    
    public LevelData()
    {
        dayCount = 1; 
    }
}