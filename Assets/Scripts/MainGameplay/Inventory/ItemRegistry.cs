
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemRegistry: MonoBehaviour
{
    public List<RegisteredItem> items = new();

    public Item getFromName(string id)
    {
        foreach (RegisteredItem registeredItem in items)
        {
            if (registeredItem.id == id)
            {
                return registeredItem.item;
            }
        }

        return null;
    }

    public string getIdFromItem(Item item)
    {
        foreach (RegisteredItem registeredItem in items)
        {
            if (registeredItem.item == item)
            {
                return registeredItem.id;
            }
        }

        return null;
    }
}

[Serializable]
public class RegisteredItem
{
    public string id;
    public Item item;
}