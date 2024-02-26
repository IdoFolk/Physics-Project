using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;

    private void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * speed * Vector3.forward);
    }
}