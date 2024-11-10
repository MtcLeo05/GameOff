using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [NonSerialized] public Registry registry;
    
    [Header("Level Stuff")]
    public int dayCount;
    public float dayTimer;
    
    public SerializableDictionary<string, CropData> crops;
    
    [Header("Player Stuff")]
    public List<SerializableStack> playerInventory;

    public GameData()
    {
        dayCount = 1; 
        dayTimer = 0;
        playerInventory = new ();
        crops = new();
    }
}

[System.Serializable]
public class SerializableStack
{
    public string id;
    public int count;

    public SerializableStack(string id, int count)
    {
        this.id = id;
        this.count = count;
    }

    public override string ToString()
    {
        return "[" + id + count+ "]";
    }
}
