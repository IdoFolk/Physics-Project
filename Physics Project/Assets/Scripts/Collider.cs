using System;
using UnityEngine;

[Serializable]
public class Collider
{
    public event Action<Collider> OnCollisionEnter;
    public event Action<Collider> OnCollisionExit;

    public ColliderType ColliderType { get; }
    public Vector3 Size { get; }
    public float Radius { get; }
    public Transform Transform { get; }

    public Collider(ColliderConfig config, Transform parent)
    {
        Size = config.Size;
        ColliderType = config.ColliderType;
        Radius = parent.localScale.x;
        Transform = parent;
    }

    public bool CheckCollision(Collider otherCollider)
    {
        if (ColliderType == ColliderType.Box && otherCollider.ColliderType == ColliderType.Box)
        {
            // Perform box-box collision check
            
        }
        else if (ColliderType == ColliderType.Sphere && otherCollider.ColliderType == ColliderType.Sphere)
        {
            // Perform sphere-sphere collision check
            var distance = Vector3.Distance(Transform.position, otherCollider.Transform.position);
            if (distance <= Radius + otherCollider.Radius)
            {
                OnCollisionEnter?.Invoke(otherCollider);
                return true;
            }

            return false;
        }
        else if (ColliderType == ColliderType.Box && otherCollider.ColliderType == ColliderType.Sphere)
        {
            // Perform box-sphere collision check
        }
        else if (ColliderType == ColliderType.Sphere && otherCollider.ColliderType == ColliderType.Box)
        {
            // Perform sphere-box collision check
        }
        return false;
    }
    
}

[Serializable]
public struct ColliderConfig
{
    public ColliderType ColliderType;
    public Vector3 Size;
    public float Radius;
    
    
}
public enum ColliderType
{
    Box,
    Sphere
}
