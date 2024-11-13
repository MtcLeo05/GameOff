using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager: InventoryManager
{
    public ItemNameDisplay itemName;

    private int selectedSlot = -1;
    
    private void Start()
    {
        changeSelectedSlot(0);
    }

    private void Update()
    {
        var f = Input.mouseScrollDelta.y;

        switch (f)
        {
            case < 0:
            {
                int toUse = selectedSlot;
                if (++toUse > 10)
                {
                    toUse = 0;
                }
            
                changeSelectedSlot(toUse);
                return;
            }
            case > 0:
            {
                int toUse = selectedSlot;
                if (--toUse < 0)
                {
                    toUse = 9;
                }
                changeSelectedSlot(toUse);
                break;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (Input.GetButtonDown("Slot" + ((i + 1) % 10)))
            {
                changeSelectedSlot(i);
                break;
            }
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

    public override void saveData(ref GameData d)
    {
        PlayerData data = d.playerData;
        
        if (data.playersInventory.ContainsKey(id))
        {
            data.playersInventory.Remove(id);
        }
        
        List<SerializableStack> items = new List<SerializableStack>();
        for (var i = 0; i < slots.Length; i++)
        {
            InventorySlot inventorySlot = slots[i];
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
        
        data.playersInventory.Add(id, new InventoryData(items));
    }

    public override void loadData(GameData d)
    {
        PlayerData data = d.playerData;
        
        if (!data.playersInventory.TryGetValue(id, out InventoryData items)) return;
        
        for (var i = 0; i < items.stacks.Count; i++)
        {
            SerializableStack item = items.stacks[i];
            if(item.id.Equals("empty")) continue;

            Item it = registry.getItemFromName(item.id);

            spawnItem(it, slots[i]).overrideCount(item.count);
        }
    }

    public InventoryItem getSelectedItem()
    {
        return slots[selectedSlot].GetComponentInChildren<InventoryItem>();
    }

    private ContainerBase lastContainer;
    public void openInventory(ContainerBase container)
    {
        GetComponent<PlayerMove>().inventoryOpen = true;    
        inventoryHud.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        if (!container)
        {
            if (lastContainer) lastContainer.close();
            lastContainer = null;
            return;
        }

        lastContainer = container;
        lastContainer.inventory.inventoryHud.SetActive(true);
    }

    public void closeInventory()
    {
        inventoryHud.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (lastContainer)
        {
            lastContainer.inventory.inventoryHud.SetActive(false);
            lastContainer.close();
        }
    }
}