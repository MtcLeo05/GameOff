using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DaySystem : MonoBehaviour, IDataPersistence
{
    [Header("Day Components")]
    public float dayDuration;
    [SerializeField] private int dayCount;
    [SerializeField] private float dayTimer;
    [SerializeField] private Light solarLight, lunarLight;
    
    [Header("HUD Components")]
    public Slider hudSlider;
    public TextMeshProUGUI hudText;
    public Image sun, moon;
    
    void Start()
    {
        solarLight = GameObject.Find("SolarLight").GetComponent<Light>();
        lunarLight = GameObject.Find("LunarLight").GetComponent<Light>();
    }

    void Update()
    {
        dayTimer += Time.deltaTime;
        
        if(dayTimer >= dayDuration)
        {
            dayTimer = 0;
            dayCount++;
        }
        
        float rot = (dayTimer / dayDuration) * 360;
        
        solarLight.transform.rotation = Quaternion.Euler(rot, solarLight.transform.rotation.y, solarLight.transform.rotation.z);
        lunarLight.transform.rotation = Quaternion.Euler(-rot, lunarLight.transform.rotation.y, lunarLight.transform.rotation.z);
        
        handleHUD();
    }

    public bool isDay()
    {
        return (dayTimer / dayDuration) < 0.55f;
    }

    void handleHUD()
    {
        float v = dayTimer / dayDuration;
        
        hudSlider.value = v;
        hudText.text = dayCount.ToString();
        
        Color sunColor = sun.color;
        Color moonColor = moon.color;
        
        if (v <= 0.45f)
        {
            sunColor.a = 1;
            moonColor.a = 0;
        }

        if (v >= 0.55f)
        {
            sunColor.a = 0;
            moonColor.a = 1;
        }
        sun.color = sunColor;
        moon.color = moonColor;
    }
    
    public void loadData(GameData data)
    {
        dayCount = data.dayCount;
        dayTimer = data.dayTimer;
    }

    public void saveData(ref GameData data)
    {
        data.dayCount = dayCount;
        data.dayTimer = dayTimer;
    }
}
