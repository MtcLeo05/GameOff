
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite image;
    public bool stackable;
    public int maxStackSize = 1;
    public GameObject droppedItem;

    public abstract bool use(ref InventoryItem item, GameObject target);
}