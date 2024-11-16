using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContainerBase : MonoBehaviour, IPlayerInteractable
{
    public Material outline;
    
    protected PlayerInventoryManager player;
    private Renderer render;
    
    private void Awake()
    {
        render = GetComponent<Renderer>();
        Material[] backup = new Material[render.materials.Length + 1];

        render.materials.CopyTo(backup, 0);
        backup[render.materials.Length] = outline;

        render.materials = backup;
    }

    public PlayerInventoryManager getPlayer()
    {
        return player;
    }

    public void highlight(bool isHovered)
    {
        if(!render) return;
        render.materials[1].SetColor(IPlayerInteractable.outlineColor, isHovered ? Color.black: Color.clear);
    }

    public void activate(GameObject g)
    {
        var inv = g.GetComponent<PlayerInventoryManager>();

        player = inv;
        player.openInventory(this);
    }

    public abstract void openInventory();

    public virtual void close()
    {
        player = null;
    }
    
    public bool state()
    {
        return player != null;
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }
    
}
