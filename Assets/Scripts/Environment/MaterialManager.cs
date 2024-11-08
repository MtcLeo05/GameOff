using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public PlayerMove player;
    public Material mat;

    public bool active;
    
    private void Start() {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        active = player.vision;
    }
}
