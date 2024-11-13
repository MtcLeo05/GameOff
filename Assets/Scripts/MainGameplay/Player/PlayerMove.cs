using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour, IDataPersistence
{
    [Header("Look")]
    public Camera playerCam;
    public float sensitivity = 100f;
    private float xRot;
    public bool vision;
    
    [Header("Move")]
    public Rigidbody playerBody;
    public float speed = 2f;
    public float jumpForce = 2f;
    public Vector3 move;
    public Vector3 smoothVelocity;

    public bool sprinting;

    [Header("Jump")]
    public Transform feet;
    public bool grounded;
    public LayerMask mask;
    public float coyote;
    public float jumpCd;

    [Header("Inventory")] [SerializeField] public bool inventoryOpen = false;
    private PlayerInventoryManager inventory;
    
    [Header("Other")]
    public Vector3 checkPointPos;
    public Quaternion checkPointRot;
    public PlayerHealth health;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerBody = GetComponent<Rigidbody>();
        checkPointPos = transform.position;
        checkPointRot = transform.rotation;
        health = GetComponent<PlayerHealth>();
        inventory = GetComponent<PlayerInventoryManager>();
    }

    void Update()
    {
        handleLook();
        handleMove();
        handleJump();
        handleInventory();
    }

    private void handleInventory()
    {
        if(!grounded) return;
        
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryOpen = !inventoryOpen;
            if (inventoryOpen)
            {
                inventory.openInventory(null);
            }
            else
            {
                inventory.closeInventory();
            }
        }
    }

    private void FixedUpdate() {
        playerBody.MovePosition(playerBody.position + transform.TransformDirection(move) * Time.fixedDeltaTime);
    }

    void handleLook()
    {
        if (inventoryOpen) return;
        
        transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity));

        xRot += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        xRot = Mathf.Clamp(xRot, -60f, 60f);
        playerCam.transform.localEulerAngles = Vector3.left * xRot;
    }

    void handleMove(){
        if (inventoryOpen)
        {
            move = Vector3.zero;
            return;
        }
        
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        sprinting = Input.GetButton("Sprint");

        Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 targetMove = moveDir * (speed * (sprinting? 2: 1));
        move = Vector3.SmoothDamp(move, targetMove, ref smoothVelocity, .15f);
    }

    void handleJump() {
        if (inventoryOpen) return;
        
        if(!grounded) {
            coyote += Time.deltaTime;
        }

        jumpCd -= Time.deltaTime;
        if(jumpCd <= 0) jumpCd = 0; 

        if(Input.GetButtonDown("Jump")) {
            if((grounded || coyote < (20 * Time.deltaTime)) && jumpCd <= 0) {
                playerBody.AddForce(transform.up * jumpForce);
                jumpCd = 30 * Time.deltaTime;
            }
        }

        Ray ray = new Ray(feet.transform.position, -transform.up);

        grounded = Physics.Raycast(ray, out _, .4f, mask);

        if(grounded) {
            coyote = 0;
        }
    }

    public void die(){
        transform.position = checkPointPos;
        transform.rotation = checkPointRot;
        health.health = health.maxHealth / 2f;
        health.stamina = health.maxStamina / 2f;
    }

    public void loadData(GameData data)
    {
        transform.position = data.playerData.playerPosition;
        checkPointPos = data.playerData.playerPosition;
    }

    public void saveData(ref GameData data)
    {
        data.playerData.playerPosition = checkPointPos;
    }
}
