using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SliderColor : MonoBehaviour
{
    public Gradient gradient;
    public Image image;
    void Start()
    {
        image = GetComponent<Image>();  
    }

    // Update is called once per frame
    void Update()
    {
        float imageFillAmount = image.fillAmount;
        
        image.color = gradient.Evaluate(imageFillAmount);
    }
}
