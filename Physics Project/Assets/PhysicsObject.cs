using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField] private float mass;
    [SerializeField] private bool usingGravity;
    public float Mass => mass;
    public Vector3 Position => transform.position;

    private bool _isColliding;
    protected List<Vector3> _forces = new List<Vector3>();
    private Vector3 _gravityForce = Vector3.zero;
    private Vector3 Velocity
    {
        get
        {
            Vector3 velocity = Vector3.zero;
            foreach (var force in _forces)
            {
                velocity += force;
            }

            return velocity;
        }
    }

    public virtual void Start()
    {
        if (usingGravity) _forces.Add(_gravityForce);
    }


    private void FixedUpdate()
    {
        transform.Translate((Velocity) * Time.fixedDeltaTime);
    }

    public void ApplyForce(Vector3 force)
    {
        //if (!usingGravity) return;
        if(_isColliding) return;
        _gravityForce = force / mass;
        //if (forceDirection.magnitude is <= 0.1f or >= -0.1f) return;
    }

    private void OnCollisionEnter(Collision other)
    {
        _isColliding = true;
    }

    private void OnCollisionExit(Collision other)
    {
        _isColliding = false;

    }
}
