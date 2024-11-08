
using System;
using TMPro;
using UnityEngine;

public class ItemNameDisplay: MonoBehaviour
{
    public TextMeshProUGUI text;
    private float lifespan = 0;
    private Color textColor;
    
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        textColor = text.color;
    }

    public void changeItemName(Item item)
    {
        text.text = item.itemName;
        gameObject.SetActive(true);
        lifespan = 100 * Time.deltaTime;
    }

    private void Update()
    {
        if (lifespan <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        
        lifespan -= Time.deltaTime;
    }
}