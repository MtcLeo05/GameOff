using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivator
{
    public abstract void highlight(bool isHovered);
    public abstract void activate(GameObject player);

    public abstract bool state();
}
