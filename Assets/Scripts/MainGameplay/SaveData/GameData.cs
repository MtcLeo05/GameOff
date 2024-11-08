using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class GameData
{
    public int dayCount;
    [FormerlySerializedAs("dayProgress")] public float dayTimer;

    public GameData()
    {
        dayCount = 1; 
        dayTimer = 0;
    }
}
