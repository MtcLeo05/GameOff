using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivator
{
    public static readonly int outlineColor = Shader.PropertyToID("_OutlineColor");
    public static readonly int outlineThickness = Shader.PropertyToID("_OutlineThickness");
    
    public void highlight(bool isHovered);
    public void activate(GameObject player);

    public bool state();

    public GameObject getGameObject();
}
