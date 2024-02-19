using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

[Serializable]
public class Collider
{
    public event Action<Collider> OnCollisionEnter;
    public event Action<Collider> OnCollisionExit;

    public ColliderType ColliderType { get; }
    public Vector3 Size { get; }
    public float Radius { get; }

    public Collider(ColliderConfig config)
    {
        Size = config.Size;
        ColliderType = config.ColliderType;
        Radius = config.Radius;
    }

    public bool CheckCollision()
    {
        return false;
    }
    
}

[Serializable]
public struct ColliderConfig
{
    public ColliderType ColliderType;
    [ShowIf(nameof(ColliderType),ColliderType.Box)]public Vector3 Size;
    [ShowIf(nameof(ColliderType),ColliderType.Sphere)]public float Radius;
    
    
}
public enum ColliderType
{
    Box,
    Sphere
}
