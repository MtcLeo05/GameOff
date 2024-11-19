using System.Collections.Generic;
using UnityEngine;

public class CropPuzzleMaster : MonoBehaviour, IPlayerInteractable
{
    public List<CropPuzzlePiece> pieces;
    public bool getComplete()
    {
        return pieces.TrueForAll(p => p.isComplete());
    }
    
    public GameObject activatable;

    private void Update()
    {
        activatable?.GetComponent<IActivatable>()?.activate(getComplete());
    }

    public bool state()
    {
        return getComplete();
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void highlight(bool isHovered)
    {
        
    }

    public void activate(GameObject player)
    {
        if(getComplete()) return;
        
        var inventory = player.GetComponent<PlayerInventoryManager>();
        if(!inventory) return;

        var item = inventory.getSelectedItem();
        if(!item) return;
        
        foreach (var piece in pieces)
        {
            piece.complete(item);
        }
    }
}
