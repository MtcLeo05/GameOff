using System;
using UnityEngine;

[Serializable]
public class CropHolder: MonoBehaviour, IDataPersistence
{
    public string id;
    public CropBase crop;

    [ContextMenu("Guid Gen")]
    private void generateGuid()
    {
        id = Guid.NewGuid().ToString();
    }
    
    public void loadData(GameData data)
    {
        if (!data.crops.TryGetValue(id, out var cData)) return;

        crop = Instantiate(data.registry.getCropFromType(cData.cropType), transform.position, transform.rotation).GetComponentInChildren<CropBase>();
        
        Vector3 pos = crop.transform.position;
        pos.x += crop.xOffset;
        pos.y += crop.yOffset;
        pos.z += crop.zOffset;
        crop.transform.position = pos;
        
        crop.transform.parent.parent = transform;
        crop.loadData(cData, data.registry);
    }

    public void saveData(ref GameData data)
    {
        if(!crop) return;
        
        CropData cData = crop.saveData(data.registry);

        if (data.crops.ContainsKey(id))
        {
            data.crops.Remove(id);
        }
        
        data.crops.Add(id, cData);
    }
}