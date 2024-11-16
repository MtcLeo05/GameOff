
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopScreen: MonoBehaviour
{
    public TextMeshProUGUI playerMoney;
    
    public List<ShopItem> shopItems;
    
    [FormerlySerializedAs("inventoryItems")] public List<InventorySlot> inventorySlots;
}
