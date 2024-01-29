using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    [SerializeField] private ColliderType colliderType;
}

public enum ColliderType
{
    Box,
    Sphere
}
