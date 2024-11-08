using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int dayCount;
    public float dayTimer;
    public List<SerializableStack> playerInventory;

    public GameData()
    {
        dayCount = 1; 
        dayTimer = 0;
        playerInventory = new ();
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
