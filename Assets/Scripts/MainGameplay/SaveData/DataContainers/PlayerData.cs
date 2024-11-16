using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [Header("Player Stuff")]
    public Vector3 playerPosition;
    public SerializableDictionary<string, InventoryData> playersInventory;
    public float health;
    public float stamina;
    public float money;
    
    public PlayerData()
    {
        playerPosition = new Vector3(-20, -10, 20);
        playersInventory = new SerializableDictionary<string, InventoryData>();
        health = 100;
        stamina = 200;
        money = 0;
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

[Serializable]
public class InventoryData
{
    public List<SerializableStack> stacks;

    public InventoryData(List<SerializableStack> stacks)
    {
        this.stacks = stacks;
    }
}