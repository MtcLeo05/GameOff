using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatGravityZone : GravityZone
{
    protected override Vector3 getGravityDirection(GameObject obj) {
        return transform.up;
    }
}
