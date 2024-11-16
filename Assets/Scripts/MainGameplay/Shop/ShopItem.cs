
using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem: MonoBehaviour
{
    public UnityEngine.UI.Button button;
    public Image image;
    
    public ShopMenu menu;
    
    public TextMeshProUGUI countText, priceText;
    public Item item;
    public int count;
    public float price;

    private void Start()
    {
        countText.text = count.ToString();
        priceText.text = $"{price:0.00}";
        button = GetComponent<UnityEngine.UI.Button>();
        
        button.onClick.AddListener(OnPointerClick);
        image.sprite = item.image;
    }

    private void OnPointerClick()
    {
        if (!menu) return;
        if (!menu.getPlayer()) return;
        MoneyHolder holder = menu.getPlayer().GetComponentInParent<MoneyHolder>();
        if(!holder) return;
        
        if(!holder.removeMoney(price)) return;
        
        for (var i = 0; i < count; i++)
        {
            if (!menu.getPlayer().addItem(item))
            {
                Instantiate(item.droppedItem, holder.transform.position, Quaternion.identity);
            }
        }
        
        menu.refreshMoney();
    }
}