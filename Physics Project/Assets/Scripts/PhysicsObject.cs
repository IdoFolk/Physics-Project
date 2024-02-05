using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField] private float mass;
    [SerializeField] private bool usingGravity;
    public float Mass => mass;
    public Vector3 Position => transform.position;

    private bool _isColliding;
    protected List<Force> _forces = new List<Force>();
    protected Force _gravityForce = new Force(Vector3.zero);
    private Force _angularForce = new Force(Vector3.zero);
    public Vector3 Velocity
    {
        get
        {
            Vector3 velocity = Vector3.zero;
            foreach (var force in _forces)
            {
                velocity += force.Value;
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
        //transform.Rotate(_angularForce.Value * Time.fixedDeltaTime);
    }

    public void ApplyGravityForce(Vector3 force)
    {
        if (!usingGravity) return;
        if (_isColliding)
        {
            _gravityForce.Value = Vector3.zero;
            return;
        }
        _gravityForce.Value = force / mass;
        //if (forceDirection.magnitude is <= 0.1f or >= -0.1f) return;
    }
    public void ApplyAngularForce(Vector3 force)
    {
        if(_isColliding) return;
        _angularForce.Value += force;
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        _isColliding = true;
    }

    private void OnTriggerExit(UnityEngine.Collider other)
    {
        _isColliding = false;
    }
}

public class Force
{
    public Vector3 Value;
    public Force(Vector3 value) => Value = value;
}
