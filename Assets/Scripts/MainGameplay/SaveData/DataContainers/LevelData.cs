using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int dayCount;
    public float dayTimer;

    public SerializableDictionary<string, bool> completedPuzzles;
    
    public LevelData()
    {
        dayCount = 1; 
        dayTimer = 0;
        completedPuzzles = new SerializableDictionary<string, bool>();
    }
}