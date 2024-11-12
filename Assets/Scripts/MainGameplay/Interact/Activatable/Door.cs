using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour, IActivatable
{

    public bool isSliding = true;

    [Header("Sliding Door")]
    public float moveSpeed = 1f;
    public Vector3 moveDirection;

    [Header("Rotating Door")]
    public float rotationSpeed = 1f;
    public Quaternion rotateDirection;

    bool state = false;

    Vector3 startPos;
    Vector3 endPos;

    Quaternion startRot;
    Quaternion endRot;

    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void Update()
    {
        if (isSliding)
        {
            endPos = state ? startPos + moveDirection : startPos;

            transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed);
            return;
        }

        endRot = state ? startRot * rotateDirection : startRot;

        Quaternion.RotateTowards(transform.rotation, endRot, rotationSpeed);
    }

    public void activate(bool state)
    {
        this.state = state;
    }

    public void activate()
    {
        activate(!state);
    }

    private void OnDrawGizmos() {
        if(!isSliding) return;
        Vector3 center = transform.position + new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(center, transform.localScale);
        Gizmos.color = Color.white;
    }
}
