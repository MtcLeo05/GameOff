
using System;

[Serializable]
public class SerializableCropDrop
{
    public SerializableStack item;
    public float chance;

    public SerializableCropDrop(SerializableStack item, float chance)
    {
        this.item = item;
        this.chance = chance;
    }
}