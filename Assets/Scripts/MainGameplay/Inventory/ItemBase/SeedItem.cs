using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedItem: Item
{
    public GameObject cropPrefab;
    
    public override bool use(ref InventoryItem item, GameObject target, GameObject source)
    {
        if (!target) return false;
        var cropHolder = target.GetComponent<CropHolder>();
        if (!cropHolder) return false;
        if (cropHolder.crop) return false;
        
        item.increaseCount(-1);

        if (item.count <= 0)
        {
            Destroy(item.gameObject);
        }

        cropHolder.plantCrop(cropPrefab);
        return true;
    }
}