using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlatform : MonoBehaviour
{
    public Mesh mesh;
    public MaterialManager manager;
    new MeshRenderer renderer;

    void Start()
    {
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        filter.mesh = mesh;

        renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = manager.mat;
    }

    void Update()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = manager.active;
    }
}
