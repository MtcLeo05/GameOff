using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public MaterialManager manager;
    public bool bugged;
    public Vector3 bugOffset;
    public Mesh cube;
    new private BoxCollider collider;

    bool createFake = true;
    
    float x, y, z;

    void Start()
    {
        manager = GameObject.Find("MaterialManager").GetComponent<MaterialManager>();
        gameObject.layer = 8;
        collider = GetComponent<BoxCollider>();
    }

    private void Update() {
        if(!bugged) return;

        gameObject.GetComponent<MeshRenderer>().enabled = !manager.active;

        if(!createFake) return;

        x = bugOffset.x / transform.localScale.x;
        y = bugOffset.y / transform.localScale.y;
        z = bugOffset.z / transform.localScale.z;

        Vector3 center = new(x, y, z);
        collider.center = center;

        GameObject hidden = new GameObject();
        var fPlatform = hidden.AddComponent<FakePlatform>();

        fPlatform.mesh = cube;
        fPlatform.manager = manager;

        hidden.transform.position = transform.position + new Vector3(bugOffset.x, bugOffset.y, bugOffset.z);
        hidden.transform.localScale = transform.localScale;
        hidden.transform.rotation = transform.rotation;
        hidden.transform.parent = transform;

        createFake = false;
    }

    private void OnDrawGizmos() {
        if(!bugged) return;

        Vector3 center = transform.position + new Vector3(bugOffset.x, bugOffset.y, bugOffset.z);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(center, transform.localScale);
        Gizmos.color = Color.white;
    }
}
