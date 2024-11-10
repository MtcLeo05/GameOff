using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [NonSerialized] public Registry registry;
    
    [Header("Level Stuff")]
    public int dayCount;
    public float dayTimer;
    
    public SerializableDictionary<string, CropData> crops;
    
    [Header("Player Stuff")]
    public Vector3 playerPosition;
    public List<SerializableStack> playerInventory;
    public float maxHealth, health, regen;
    public float maxStamina, stamina, staminaDrain;

    public GameData()
    {
        dayCount = 1; 
        dayTimer = 0;

        playerPosition = new Vector3(-20, -10, 20);
        playerInventory = new List<SerializableStack>();
        maxHealth = health = 100;
        regen = 1f;
        maxStamina = stamina = 50;
        staminaDrain = 1f;
        
        crops = new SerializableDictionary<string, CropData>();
    }
}

[Serializable]
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
