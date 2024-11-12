using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    private Rigidbody body;
    [SerializeField] private Vector3 target;
    [SerializeField] private float gravity = 9.81f;

    int lastGravityPriority;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.useGravity = false;
    }

    void FixedUpdate() {
        if(target == Vector3.zero) target = Vector3.up;

        Vector3 difference = target.normalized;

        body.rotation = Quaternion.FromToRotation(body.transform.up, difference) * body.rotation;


        body.AddForce(-difference * gravity * body.mass);
    }

    public void setDirection(Vector3 dir, int priority) {
        if(priority < lastGravityPriority) return;

        lastGravityPriority = priority;
        target = dir;
    }

    public void removeDirection(int priority) {
        if(priority < lastGravityPriority) return;

        lastGravityPriority = priority;
        target = Vector3.up;
    }
}
