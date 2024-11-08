using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Look")]
    public Camera playerCam;
    public float sensitivity = 100f;
    private float xRot = 0f;
    public bool vision = false;


    [Header("Move")]
    public Rigidbody playerBody;
    public float speed = 2f;
    public float jumpForce = 2f;
    public Vector3 move;
    public Vector3 smoothVelocity;

    [Header("Jump")]
    public Transform feet;
    public bool grounded = false;
    public LayerMask mask;
    public float coyote = 0f;
    public float jumpCD = 0f;

    [Header("Other")]

    public Vector3 checkPointPos;
    public Quaternion checkPointRot;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerBody = GetComponent<Rigidbody>();
        checkPointPos = transform.position;
        checkPointRot = transform.rotation;
    }

    void Update()
    {
        handleLook();
        handleMove();
        handleJump();
    }

    private void FixedUpdate() {
        playerBody.MovePosition(playerBody.position + transform.TransformDirection(move) * Time.fixedDeltaTime);
    }

    void handleLook(){
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity);

        xRot += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        xRot = Mathf.Clamp(xRot, -60f, 60f);
        playerCam.transform.localEulerAngles = Vector3.left * xRot;

        if(Input.GetButtonDown("Vision")) {
            vision = !vision;
        }
    }

    void handleMove(){
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        bool sprint = Input.GetButton("Sprint");

        Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 targetMove = moveDir * speed * (sprint? 2: 1);
        move = Vector3.SmoothDamp(move, targetMove, ref smoothVelocity, .15f);
    }

    void handleJump() {
        if(!grounded) {
            coyote += Time.deltaTime;
        }

        jumpCD -= Time.deltaTime;
        if(jumpCD <= 0) jumpCD = 0; 

        if(Input.GetButtonDown("Jump")) {
            if((grounded || coyote < (20 * Time.deltaTime)) && jumpCD <= 0) {
                playerBody.AddForce(transform.up * jumpForce);
                jumpCD = 30 * Time.deltaTime;
            }
        }

        Ray ray = new Ray(feet.transform.position, -transform.up);
        RaycastHit hit;

        grounded = Physics.Raycast(ray, out hit, .4f, mask);

        if(grounded) {
            coyote = 0;
        }
    }

    public void die(){
        transform.position = checkPointPos;
        transform.rotation = checkPointRot;
    }
}
