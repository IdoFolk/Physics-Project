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
                
                physicsObject.ApplyGravityForce(ApplyGravityForceBetweenBodies(physicsObject, otherPhysicsObject));
                var distance = CalculateDistance(physicsObject, otherPhysicsObject);
                physicsObject.ApplyAngularForce(Vector3.Distance(physicsObject.Position,otherPhysicsObject.Position),distance.normalized);
            }
        }
    }

    private Vector3 ApplyGravityForceBetweenBodies(PhysicsObject physicsObject1,PhysicsObject physicsObject2)
    {
        float massProduct = physicsObject1.Mass * physicsObject2.Mass;
        float distance = Vector3.Distance(physicsObject1.Position, physicsObject2.Position);
        Vector3 direction = physicsObject2.Position - physicsObject1.Position;
        return (massProduct / (distance * distance)) * gravityScale * direction.normalized;
    }
    private Vector3 CalculateDistance(PhysicsObject physicsObject1,PhysicsObject physicsObject2)
    {
        var distance = physicsObject1.Position - physicsObject2.Position;
        return distance;
    }
    
}
