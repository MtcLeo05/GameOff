using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDoor : MonoBehaviour, IPlayerInteractable
{
    public float moveRange = 1f;
    public float moveSpeed = 1f;
    public Vector3 moveDirection;
    public bool isSliding = true;

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
        if(isSliding)
        {
            endPos = state ? startPos + moveDirection * moveRange : startPos;

            transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed);
            return;
        }

        endRot = state ? startRot * Quaternion.Euler(0, 90, 0) : startRot;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, endRot, 1f);
    }


    public void highlight(bool isHovered)
    {
        
    }

    public void activate(GameObject player)
    {
        state = !state;
    }

    bool IActivator.state()
    {
        return state;
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }
}
