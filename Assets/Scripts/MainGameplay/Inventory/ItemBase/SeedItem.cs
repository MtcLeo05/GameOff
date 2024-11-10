using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedItem: Item
{
    public GameObject cropPrefab;
    
    public override bool use(ref InventoryItem item, GameObject target)
    {
        if (!target) return false;
        var cropHolder = target.GetComponent<CropHolder>();
        if (!cropHolder) return false;
        
        item.increaseCount(-1);

        if (item.count <= 0)
        {
            Destroy(item.gameObject);
        }

        var cropParent = Instantiate(cropPrefab, target.transform.position, Quaternion.identity);
        
        CropBase crop = cropParent.GetComponentInChildren<CropBase>();
        Vector3 pos = crop.transform.position;
        pos.x += crop.xOffset;
        pos.y += crop.yOffset;
        pos.z += crop.zOffset;
        crop.transform.position = pos;
        
        crop.transform.parent.parent = target.transform;
        cropHolder.crop = crop;

        return true;
    }
}