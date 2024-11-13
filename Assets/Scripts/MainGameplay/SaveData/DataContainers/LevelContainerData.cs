using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelContainerData
{
    [Header("Container Stuff")]
    public SerializableDictionary<string, InventoryData> containerInventory;
    
    public LevelContainerData()
    {
        containerInventory = new SerializableDictionary<string, InventoryData>(); 
    }
}