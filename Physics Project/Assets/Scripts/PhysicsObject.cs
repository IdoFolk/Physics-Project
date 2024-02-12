using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField] private float mass;
    [SerializeField] private bool usingGravity;
    [SerializeField] private float speedCheckTime;
    public float Mass => mass;
    public float Speed => _speed;
    public Vector3 Position => transform.position;

    private bool _isColliding;
    protected List<Force> _forces = new List<Force>();
    protected Force _gravityForce = new Force(Vector3.zero);
    private Force _angularForce = new Force(Vector3.zero);
    private float _speed;


    private Vector3 _lastPos;
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
        if (usingGravity) _forces.Add(_angularForce);
    }


    private void FixedUpdate()
    {
        transform.position += (Velocity) * Time.fixedDeltaTime;
        CalculateSpeed();
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
        
    }
    public void ApplyAngularForce(float radius, Vector3 direction)
    {
        if(_isColliding) return;
        var force = (Speed * Speed * mass / radius) * direction;
        _angularForce.Value += force / mass;
    }

    public void CalculateSpeed()
    {
        var distance = Vector3.Distance(_lastPos, Position);
        _speed = distance;
        Debug.Log(_speed);
        _lastPos = Position;
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
