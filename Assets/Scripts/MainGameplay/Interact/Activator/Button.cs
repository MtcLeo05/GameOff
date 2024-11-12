using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Button : MonoBehaviour, IPlayerInteractable
{
    public Material outline;
    public GameObject activatableObj;
    
    IActivatable activatable;

    bool bState = false;

    void Start()
    {
        if (activatableObj.GetComponent<IActivatable>() != null) activatable = activatableObj.GetComponent<IActivatable>();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null) return;

        outline.SetColor("_OutlineColor", Color.clear);
        outline.SetFloat("_OutlineThickness", 0.05f);

        Material[] backup = new Material[2];

        backup[0] = meshRenderer.materials[0];
        backup[1] = outline;

        meshRenderer.materials = backup;
    }

    public void activate(GameObject player)
    {
        bState = !bState;
        if(activatable != null) activatable.activate();
    }

    public void highlight(bool isHovered)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null) return;

        meshRenderer.materials[1].SetColor("_OutlineColor", isHovered ? Color.black: Color.clear);
    }

    public bool state()
    {
        return bState;
    }
}
