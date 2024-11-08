using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GravityZone : MonoBehaviour
{
    public int priority;

    private void Start() {
        if(GetComponent<Collider>()) {
            GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Gravity g;
        if(g = other.GetComponent<Gravity>()) {
            g.setDirection(getGravityDirection(other.gameObject), priority);
        }
    }

    private void OnTriggerExit(Collider other) {
        Gravity g;
        if(g = other.GetComponent<Gravity>()) {
            g.removeDirection(priority);
        }
    }

    protected virtual Vector3 getGravityDirection(GameObject obj) {
        return Vector3.up;
    }
}
