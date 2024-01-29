using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    [SerializeField] private PhysicsObject[] physicsObjects;
    [SerializeField] private float gravityScale;
    private void OnValidate()
    {
        physicsObjects = FindObjectsByType<PhysicsObject>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);
    }

    private void FixedUpdate()
    {
        foreach (var physicsObject in physicsObjects)
        {
            foreach (var otherPhysicsObject in physicsObjects)
            {
                if (physicsObject == otherPhysicsObject) continue;
                
                physicsObject.ApplyGravityForce(ApplyForceBetweenBodies(physicsObject, otherPhysicsObject));
            }
        }
    }

    private Vector3 ApplyForceBetweenBodies(PhysicsObject physicsObject1,PhysicsObject physicsObject2)
    {
        var massProduct = physicsObject1.Mass * physicsObject2.Mass;
        var distance = Vector3.Distance(physicsObject1.Position, physicsObject2.Position);
        var direction = physicsObject2.Position - physicsObject1.Position;
        return (massProduct / (distance * distance)) * gravityScale * direction.normalized;
    }
}
