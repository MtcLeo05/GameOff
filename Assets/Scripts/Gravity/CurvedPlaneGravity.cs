using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VHACD.Unity;

public class CurvedPlaneGravity : GravityZone
{
    public ComplexCollider meshCollider;

    protected override Vector3 getGravityDirection(GameObject obj) {
        Collider toUse = meshCollider.Colliders[0];
        float distance = Vector3.Distance(meshCollider.Colliders[0].ClosestPoint(obj.transform.position),  obj.transform.position);

        meshCollider.Colliders.ForEach(coll => {
            float newDistance = Vector3.Distance(coll.ClosestPoint(obj.transform.position),  obj.transform.position);

            if(newDistance <= distance) {
                distance = newDistance;
                toUse = coll;
            }
        });

        return -(toUse.ClosestPoint(obj.transform.position) - obj.transform.position);
    }
}
