using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    void Start()
    {
        gameObject.layer = 8;
    }

    private void OnTriggerEnter(Collider other) {
        PlayerMove player;

        if(player = other.GetComponent<PlayerMove>()) {
            player.checkPointPos = transform.position;
            player.checkPointRot = transform.rotation;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, transform.localScale);
        Gizmos.color = Color.white;
    }
}
