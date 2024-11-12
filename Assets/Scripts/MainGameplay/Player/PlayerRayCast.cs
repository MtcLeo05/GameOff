using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public GameObject head;
    public float range = 10f;
    public LayerMask layer;

    GameObject old;
    Vector3 pos;
    
    void Update()
    {
        handleInteractable();
        handleUseItem();
    }

    private void handleUseItem()
    {
        if (!Input.GetButtonDown("UseItem")) return;
        
        PlayerInventoryManager playerInventory = gameObject.GetComponent<PlayerInventoryManager>();
        if(!playerInventory) return;

        pos = head.transform.position;

        Ray ray = new Ray(head.transform.position, head.transform.forward);

        Debug.DrawRay(pos, head.transform.forward * range, Color.red);

        var inventoryItem = playerInventory.getSelectedItem();
        if (!inventoryItem) return;
        if (!inventoryItem.item) return;
            
        if (Physics.Raycast(ray, out RaycastHit hitInfo, range, layer)) 
        {
            if (inventoryItem.item.use(ref inventoryItem, hitInfo.transform.gameObject, gameObject))
            {
                return;
            }
        }
        
        inventoryItem.item.use(ref inventoryItem, null, gameObject);
    }

    private void handleInteractable() {
        pos = head.transform.position;

        Ray ray = new Ray(head.transform.position, head.transform.forward);

        Debug.DrawRay(pos, head.transform.forward * range, Color.red);

        if (!Physics.Raycast(ray, out RaycastHit hitInfo, range, layer)) {
            if (old) old.GetComponent<IPlayerInteractable>().highlight(false);
            old = null;
            return;
        }

        if (hitInfo.collider.GetComponent<IPlayerInteractable>() == null)
        {
            if (old) old.GetComponent<IPlayerInteractable>().highlight(false);
            old = null;
            return;
        }

        IPlayerInteractable interactable = hitInfo.collider.GetComponent<IPlayerInteractable>();

        if (old != null && old != hitInfo.collider.gameObject) old.GetComponent<IPlayerInteractable>().highlight(false);
        interactable.highlight(true);

        if(Input.GetButtonDown("Interact")) interactable.activate(gameObject);

        old = hitInfo.collider.gameObject;
    }
}
