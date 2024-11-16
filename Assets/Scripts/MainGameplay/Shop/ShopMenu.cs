using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopMenu: ContainerBase
{
    public ShopScreen shopScreen;

    private void Start()
    {
        foreach (var item in shopScreen.shopItems)
        {
            item.menu = this;
        }
    }

    public override void openInventory()
    {
        shopScreen.gameObject.SetActive(true);
        refreshMoney();
    }

    public void refreshMoney()
    {
        shopScreen.playerMoney.text = "Money: " + $"{player.gameObject.GetComponent<MoneyHolder>().getMoney():0.00}";
    }

    public override void close()
    {
        foreach (var slot in shopScreen.inventorySlots)
        {
            var item = slot.getItem();
            if(!item) continue;

            if (item.item.sellPrice != 0)
            {
                player.gameObject.GetComponent<MoneyHolder>().addMoney(item.item.sellPrice * item.count);
            }
            
            Destroy(slot.gameObject);
        }

        shopScreen.gameObject.SetActive(false);
        base.close();
    }
}
