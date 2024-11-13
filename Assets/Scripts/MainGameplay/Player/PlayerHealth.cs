
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMove))]
public class PlayerHealth: MonoBehaviour, IDataPersistence
{
    public Slider healthSlider, staminaSlider;
    
    [Header("Health Config")] 
    public float maxHealth = 100;
    public float health = 100;
    public float regen = 1f;

    [Header("Stamina Config")] 
    public float maxStamina = 200;
    public float stamina = 200;
    public float staminaDrain = 1f;
    
    PlayerMove player;

    private void Start()
    {
        player = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        healthSlider.value = health / maxHealth;
        staminaSlider.value = stamina / maxStamina;
        
        if(health <= 0) player.die();
        if (stamina < staminaDrain)
        {
            health -= staminaDrain;
            return;
        }
        
        stamina -= staminaDrain * (health <= maxHealth? 1f: 0.25f) * Time.deltaTime * (player.sprinting ? 2 : 1);
        health += regen * Time.deltaTime * (player.sprinting ? 4 : 1);
        
        health = Mathf.Clamp(health, 0, maxHealth);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    public void loadData(GameData data)
    {
        health = data.playerData.health;
        stamina = data.playerData.stamina;
    }

    public void saveData(ref GameData data)
    {
        data.playerData.health = health;
        data.playerData.stamina = stamina;
    }
}