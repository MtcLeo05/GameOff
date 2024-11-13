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

    public CropBase plantCrop(GameObject newCrop)
    {
        CropBase crop = Instantiate(newCrop, transform.position, Quaternion.identity).GetComponentInChildren<CropBase>();

        crop.transform.parent.position = transform.position + (transform.rotation * crop.offset);
        crop.transform.parent.rotation = transform.rotation;
        crop.transform.parent.parent = transform;

        this.crop = crop;
        
        return crop;
    }
    
    public void loadData(GameData data)
    {
        LevelCropData d = data.levelCropData;
        if (!d.crops.TryGetValue(id, out var cData)) return;

        crop = plantCrop(data.registry.getCropFromType(cData.cropType));
        crop.loadData(cData, data.registry);
    }

    public void saveData(ref GameData data)
    {
        LevelCropData d = data.levelCropData;
        if(!crop) return;
        
        CropData cData = crop.saveData();

        if (d.crops.ContainsKey(id))
        {
            d.crops.Remove(id);
        }
        
        d.crops.Add(id, cData);
    }
}