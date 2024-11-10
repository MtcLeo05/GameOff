using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedItem: Item
{
    public GameObject crop;
    
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

        var cropObj = Instantiate(crop, target.transform.position, Quaternion.identity);
        cropObj.transform.parent = target.transform;
        
        CropBase cropI = cropObj.GetComponentInChildren<CropBase>();
        Vector3 pos = crop.transform.position;
        pos.x += cropI.xOffset;
        pos.y += cropI.yOffset;
        pos.z += cropI.zOffset;
        crop.transform.position = pos;
        cropHolder.crop = cropI;

        return true;
    }
}