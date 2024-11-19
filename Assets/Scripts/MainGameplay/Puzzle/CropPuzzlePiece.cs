
using System;
using UnityEngine;

public class CropPuzzlePiece: MonoBehaviour, IDataPersistence
{
    public string id;

    [ContextMenu("Generate GUID")]
    public void generateId()
    {
        id = Guid.NewGuid().ToString();
    }

    public Item item;
    public Transform[] completedPosition;
    protected bool completed;
    
    public virtual void complete(InventoryItem newItem)
    {
        if(item != newItem.item) return;
        newItem.increaseCount(-1);
        completed = true;
        spawnItems();
    }

    void spawnItems()
    {
        foreach (var t in completedPosition)
        {
            var i = Instantiate(item.droppedItem.gameObject, t, true);
            i.transform.localPosition = Vector3.zero;
            i.transform.localRotation = Quaternion.identity;
            i.transform.localScale *= .5f;
            if (i.GetComponentInChildren<ItemDrop>()) Destroy(i.GetComponentInChildren<ItemDrop>());
            if (i.GetComponent<Gravity>()) Destroy(i.GetComponent<Gravity>());
            if (i.GetComponent<Rigidbody>()) Destroy(i.GetComponent<Rigidbody>());
            if (i.GetComponentInChildren<Collider>()) Destroy(i.GetComponentInChildren<Collider>());
        }
    }
    
    public virtual bool isComplete()
    {
        return completed;
    }

    public virtual void loadData(GameData data)
    {
        if (!data.levelData.completedPuzzles.TryGetValue(id, out var c)) return;
        completed = c;
        
        if(!completed) return;
        spawnItems();
    }

    public virtual void saveData(ref GameData data)
    {
        if (data.levelData.completedPuzzles.TryAdd(id, completed)) return;
        data.levelData.completedPuzzles[id] = completed;
    }
    
}