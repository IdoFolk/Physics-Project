using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
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
    public Collider Collider { get; private set; }
    public float Mass => _mass;
    public Vector3 Speed => _speed;
    public Vector3 Position => transform.position;
    
    [SerializeField] private float _mass;
    [SerializeField] private bool _usingGravity;
    [SerializeField] private bool _hasCollider;
    [SerializeField] private ColliderConfig _colliderConfig;
    

    private bool _isColliding;
    protected List<Force> _forces = new List<Force>();
    protected Force _gravityForce = new Force(Vector3.zero);
    private Force _normalForce = new Force(Vector3.zero);
    private Vector3 _speed;
    private Vector3 _lastPos;
    

    private void Awake()
    {
        if (_hasCollider) Collider = new Collider(_colliderConfig,transform);
    }

    public virtual void Start()
    {
        _lastPos = Position;
        if (_usingGravity) _forces.Add(_gravityForce);
        if (_hasCollider) _forces.Add(_normalForce);
    }


    protected virtual void FixedUpdate()
    {
        _speed += (Velocity / _mass) * Time.fixedDeltaTime;
        transform.position += _speed * Time.fixedDeltaTime;
    }

    public void ApplyGravityForce(Vector3 force)
    {
        if (!_usingGravity) return;
        _gravityForce.Value = force;
        
    }

    public void CheckCollision(PhysicsObject otherPhysicsObject)
    {
        var distance = Vector3.Distance(Position, otherPhysicsObject.Position);
        if (!(distance < (Collider.Radius + otherPhysicsObject.Collider.Radius) * 2)) return;
        
        if (Collider.CheckCollision(otherPhysicsObject.Collider))
        {
            ApplyNormalForce();
        }
    }

    private void ApplyNormalForce()
    {
        _speed = -_speed / _mass;
    }

    private void OnDrawGizmosSelected()
    {
        if (_hasCollider)
        {
            switch (_colliderConfig.ColliderType)
            {
                case ColliderType.Box:
                    Gizmos.DrawCube(transform.position,_colliderConfig.Size);
                    break;
                case ColliderType.Sphere:
                    Gizmos.DrawSphere(transform.position,_colliderConfig.Radius);
                    break;
            }
        }
    }
}

public class Force
{
    public Vector3 Value;
    public Force(Vector3 value) => Value = value;
}
