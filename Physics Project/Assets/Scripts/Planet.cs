using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : PhysicsObject
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private bool _destroyOnImpact;

    protected override void Start()
    {
        base.Start();
        OnCollisionEnter += DestroyOtherObject;
    }

    protected override void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,_rotateSpeed,0));
        base.FixedUpdate();
    }

    private void DestroyOtherObject(PhysicsObject other)
    {
        if(_destroyOnImpact)
            other.DestroyObject();
        
    }
}
