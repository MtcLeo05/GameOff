using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = 8;
    }

    private void OnTriggerEnter(Collider other) {
        PlayerMove player;

        if(player = other.GetComponent<PlayerMove>()) {
            player.die();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
        Gizmos.color = Color.white;
    }
}
