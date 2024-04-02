using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : PhysicsObject
{
    [SerializeField] private float _rotateSpeed;
    protected override void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,_rotateSpeed,0));
        base.FixedUpdate();
    }
}
