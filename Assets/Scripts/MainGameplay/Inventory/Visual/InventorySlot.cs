using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot: MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedC, defaultC;

    private void Awake()
    {
        deselect();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0)
        {
            InventoryItem heldItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventoryItem slotItem = GetComponentInChildren<InventoryItem>();

            if (heldItem.item == slotItem.item && heldItem.item.stackable &&
                heldItem.count + slotItem.count < heldItem.item.maxStackSize)
            {
                slotItem.increaseCount(heldItem.count);
                Destroy(heldItem.gameObject);
            }
            
            return;
        }
        
        InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
        item.parentAfterDrag = transform;
    }

    public void select()
    {
        image.color = selectedC;
    }

    public void deselect()
    {
        image.color = defaultC;
    }
}