using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    [Header("UI")] 
    public Image image;
    public TextMeshProUGUI text;

    [HideInInspector] public Transform parentAfterDrag;
    public Item item;
    public int count = 1;

    public void initItem(Item newItem)
    {
        item = newItem;
        image.sprite = item.image;
        refreshCount();
    }

    public void increaseCount(int amount)
    {
        count += amount;
        refreshCount();
    }

    public void overrideCount(int amount)
    {
        this.count = amount;
        refreshCount();
    }

    public void refreshCount()
    {
        if (count > 1 && item.stackable)
        {
            text.gameObject.SetActive(true);
            text.text = count.ToString();
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}