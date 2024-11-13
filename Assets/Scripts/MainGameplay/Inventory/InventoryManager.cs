using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager: MonoBehaviour, IDataPersistence
{
    public string id;

    [ContextMenu("Guid Gen")]
    private void generateGuid()
    {
        id = Guid.NewGuid().ToString();
    }
    
    public Registry registry;
    
    public GameObject inventoryHud;
    public InventorySlot[] slots;
    public GameObject itemPrefab;
    
    public bool addItem(Item item)
    {
        foreach (InventorySlot slot in slots)
        {
            InventoryItem slotItem = slot.GetComponentInChildren<InventoryItem>();
            if (slotItem != null && slotItem.item == item && slotItem.item.stackable && slotItem.count < slotItem.item.maxStackSize)
            {
                slotItem.increaseCount(1);
                return true;
            }
        }
        
        foreach (InventorySlot slot in slots)
        {
            InventoryItem slotItem = slot.GetComponentInChildren<InventoryItem>();
            if (slotItem == null)
            {
                spawnItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public InventoryItem spawnItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(itemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.initItem(item);
        return inventoryItem;
    }

    public virtual void loadData(GameData d)
    {
        LevelContainerData data = d.levelContainerData;
        
        if (data.containerInventory.TryGetValue(id, out InventoryData items))
        {
            for (var i = 0; i < items.stacks.Count; i++)
            {
                SerializableStack item = items.stacks[i];
                if(item.id.Equals("empty")) continue;

                Item it = registry.getItemFromName(item.id);

                spawnItem(it, slots[i]).overrideCount(item.count);
            }
        }
    }

    public virtual void saveData(ref GameData d)
    {
        LevelContainerData data = d.levelContainerData;
        
        if (data.containerInventory.ContainsKey(id))
        {
            data.containerInventory.Remove(id);
        }
        
        List<SerializableStack> items = new List<SerializableStack>();
        foreach (var inventorySlot in slots)
        {
            InventoryItem item = inventorySlot.GetComponentInChildren<InventoryItem>();

            SerializableStack stack;
            
            if (item == null)
            {
                stack = new SerializableStack("empty", 0);
                items.Add(stack);
                continue;
            }

            string itemId = registry.getIdFromItem(item.item);
            stack = new SerializableStack(itemId, item.count);
            items.Add(stack);
        }
        
        data.containerInventory.Add(id, new InventoryData(items));
    }
}