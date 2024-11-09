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
    }

    void handleInteractable() {
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

        if (old != null && old != hitInfo.collider) old.GetComponent<IPlayerInteractable>().highlight(false);
        interactable.highlight(true);

        if(Input.GetButtonDown("Interact")) interactable.activate(gameObject);

        old = hitInfo.collider.gameObject;
    }
}
