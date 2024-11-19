
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Generic")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite image;
    public bool stackable;
    public int maxStackSize = 1;
    public GameObject droppedItem;
    public GameObject puzzlePrefab;
    public float sellPrice = 0;

    public virtual bool use(ref InventoryItem item, GameObject target, GameObject source)
    {
        return false;
    }
}