
using System;
using System.Collections.Generic;
using UnityEngine;

public class Registry: MonoBehaviour
{
    public List<RegisteredItem> items = new();

    public Item getItemFromName(string id)
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

    public List<RegisteredCrop> crops = new();

    public GameObject getCropFromType(CropType type)
    {
        foreach (RegisteredCrop crop in crops)
        {
            if (crop.type == type)
            {
                return crop.crop;
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

[Serializable]
public class RegisteredCrop{
    public CropType type;
    public GameObject crop;
}