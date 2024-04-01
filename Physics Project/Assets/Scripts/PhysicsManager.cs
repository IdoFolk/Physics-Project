using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public static float GravityScale = 100;
    [SerializeField] private PhysicsObject[] physicsObjects;
    [SerializeField] private PhysicsObject spaceship;
    private void OnValidate()
    {
        physicsObjects = FindObjectsByType<PhysicsObject>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);
    }
    
    private void FixedUpdate()
    {
        foreach (var physicsObject1 in physicsObjects)
        {
            foreach (var physicsObject2 in physicsObjects)
            {
                // Skip interaction with itself
                if (physicsObject1 == physicsObject2) continue;

                Vector3 gravityForce = ApplyGravityForceBetweenBodies(physicsObject1, physicsObject2);
                physicsObject1.ApplyGravityForce(physicsObject2.GetInstanceID(), gravityForce);
                physicsObject1.CheckCollision(physicsObject2);
            }
        }
    }

    private Vector3 ApplyGravityForceBetweenBodies(PhysicsObject physicsObject1,PhysicsObject physicsObject2)
    {
        float massProduct = physicsObject1.Mass * physicsObject2.Mass;
        float distance = Vector3.Distance(physicsObject1.Position, physicsObject2.Position);
        Vector3 direction = physicsObject2.Position - physicsObject1.Position;
        return (massProduct / (distance * distance)) * GravityScale * direction.normalized;
    }
    private Vector3 CalculateDistance(PhysicsObject physicsObject1,PhysicsObject physicsObject2)
    {
        var distance = physicsObject1.Position - physicsObject2.Position;
        return distance;
    }
    
}
