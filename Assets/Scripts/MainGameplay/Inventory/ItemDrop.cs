
using UnityEngine;

public class ItemDrop: MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInventoryManager>())
        {
            PlayerInventoryManager playerInventoryManager = other.GetComponent<PlayerInventoryManager>();
            if(playerInventoryManager.addItem(item)) Destroy(transform.parent.gameObject);
        }
    }
}