using UnityEngine;

public interface IPlayerInteractable : IActivator
{
    public void highlight(bool isHovered);
    public void activate(GameObject player);
}
