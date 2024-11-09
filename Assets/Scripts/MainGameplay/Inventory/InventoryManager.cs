using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager: MonoBehaviour, IDataPersistence
{
    public ItemRegistry registry;
    
    public InventorySlot[] slots;
    public GameObject itemPrefab;
    
    public ItemNameDisplay itemName;

    private int selectedSlot = -1;

    private void Start()
    {
        registry = FindObjectOfType<ItemRegistry>();
        changeSelectedSlot(0);
    }

    private void Update()
    {
        float f = Input.mouseScrollDelta.y;

        if (f < 0)
        {
            int toUse = selectedSlot;
            if (++toUse > 10)
            {
                toUse = 0;
            }
            
            changeSelectedSlot(toUse);
            return;
        }

        if (f > 0)
        {
            int toUse = selectedSlot;
            if (--toUse < 0)
            {
                toUse = 9;
            }
            changeSelectedSlot(toUse);
        }

        if (Input.GetButtonDown("UseItem"))
        {
            InventoryItem inventoryItem = getSelectedItem();
            
            if(inventoryItem != null) inventoryItem.item.use(ref inventoryItem);
        }
    }

    void changeSelectedSlot(int newValue)
    {
        if(selectedSlot >= 0) slots[selectedSlot].deselect();
        slots[newValue].select();
        selectedSlot = newValue;

        if (getSelectedItem())
        {
            itemName.changeItemName(getSelectedItem().item);
        }
    }
    
    public InventoryItem getSelectedItem()
    {
        return slots[selectedSlot].GetComponentInChildren<InventoryItem>();
    }
    
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

    public void loadData(GameData data)
    {
        for (var i = 0; i < data.playerInventory.Count; i++)
        {
            SerializableStack item = data.playerInventory[i];
            if(item.id.Equals("empty")) continue;

            Item it = registry.getFromName(item.id);

            spawnItem(it, slots[i]).overrideCount(item.count);
        }
    }

    public void saveData(ref GameData data)
    {
        data.playerInventory.Clear();
        
        for (var i = 0; i < slots.Length; i++)
        {
            InventorySlot inventorySlot = slots[i];
            InventoryItem item = inventorySlot.GetComponentInChildren<InventoryItem>();

            SerializableStack stack;
            
            if (item == null)
            {
                stack = new SerializableStack("empty", 0);
                data.playerInventory.Add(stack);
                continue;
            }

            string id = registry.getIdFromItem(item.item);
            stack = new SerializableStack(id, item.count);
            data.playerInventory.Add(stack);
        }
    }
}