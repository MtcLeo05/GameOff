
using UnityEngine;

public class ItemDrop: MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InventoryManager>())
        {
            InventoryManager inventoryManager = other.GetComponent<InventoryManager>();
            if(inventoryManager.addItem(item)) Destroy(transform.parent.gameObject);
        }
    }
}