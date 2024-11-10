
using System;

[Serializable]
public class CropDrop
{
    public Item item;
    public int count;
    public float chance;

    public CropDrop(Item item, int count, float chance)
    {
        this.item = item;
        this.count = count;
        this.chance = chance;
    }
    
}
