using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public SumForces Velocity { get; private set; } = new(0);

    public SumForces GravityForces { get; private set; } = new(1);
    public Collider Collider { get; private set; }
    public float Mass => _mass;
    public float Bounciness => _bounciness;
    public Vector3 Speed => _speed;
    public Vector3 Position => transform.position;

    [SerializeField] private float _mass;
    [SerializeField, Range(0, 2)] private float _bounciness;
    [SerializeField] private bool _isMoveable;
    [SerializeField] private bool _usingGravity;
    [SerializeField] private bool _hasCollider;
    [SerializeField] private ColliderConfig _colliderConfig;
    [SerializeField, Range(0, 0.1f)] private float _penetrationTolerance;


    private bool _isColliding;
    private Vector3 _speed;
    private Vector3 _lastPos;


    private void Awake()
    {
        if (_hasCollider) Collider = new Collider(_colliderConfig, transform);
    }

    public virtual void Start()
    {
        _lastPos = Position;
        if (_usingGravity) Velocity.AddForce(GravityForces);
    }


    protected virtual void FixedUpdate()
    {
        _speed += (Velocity.Value / _mass) * Time.fixedDeltaTime;
        transform.position += _speed * Time.fixedDeltaTime;
    }

    public void ApplyGravityForce(int objectID, Vector3 force)
    {
        if (!_usingGravity) return;
        GravityForces.ChangeForce(objectID, force);
    }

    public void CheckCollision(PhysicsObject otherPhysicsObject)
    {
        var distance = Vector3.Distance(Position, otherPhysicsObject.Position);
        if (!(distance < (Collider.Radius + otherPhysicsObject.Collider.Radius) * 2)) return;

        if (Collider.CheckCollision(otherPhysicsObject.Collider))
        {
            // Calculate collision normal
            Vector3 collisionNormal = (Position - otherPhysicsObject.Position).normalized;
            ApplyNormalForce(collisionNormal);

            // Resolve penetration by adjusting positions
            float penetrationDepth = (Collider.Radius + otherPhysicsObject.Collider.Radius) - distance;
            if (penetrationDepth > _penetrationTolerance)
            {
                Vector3 correctionVector = collisionNormal * penetrationDepth;
                if (_isMoveable) transform.position += correctionVector * 0.5f / _mass; // Apply half of the correction to each object
                if (otherPhysicsObject._isMoveable) otherPhysicsObject.transform.position -= (correctionVector * 0.5f) / _mass;
            }
        }
    }

    private void ApplyNormalForce(Vector3 collisionNormal)
    {
        _speed = Vector3.Reflect(_speed, collisionNormal) * _bounciness;
    }

    private void OnDrawGizmosSelected()
    {
        if (_hasCollider)
        {
            switch (_colliderConfig.ColliderType)
            {
                case ColliderType.Box:
                    Gizmos.DrawCube(transform.position, _colliderConfig.Size);
                    break;
                case ColliderType.Sphere:
                    Gizmos.DrawSphere(transform.position, _colliderConfig.Radius);
                    break;
            }
        }
    }
}

public class Force
{
    public int ID;
    public virtual Vector3 Value { get; set; }
    public Force(int id) => ID = id;
    public Force(Vector3 value) => Value = value;
}

public class SumForces : Force
{
    private Dictionary<int, Force> _forces = new();

    public SumForces(int id) : base(id)
    {
    }

    public override Vector3 Value
    {
        get
        {
            Vector3 sum = Vector3.zero;
            foreach (var force in _forces)
            {
                sum += force.Value.Value;
            }

            return sum;
        }
    }

    public void AddForce(Force force)
    {
        _forces.Add(force.ID, force);
    }

    public void ChangeForce(int id, Vector3 value)
    {
        if (_forces.TryGetValue(id,out var force))
        {
            force.Value = value;
        }
        else
        {
            _forces.Add(id,new Force(value));
        }
    }

    public void RemoveForce(int id)
    {
        if (!_forces.ContainsKey(id)) return;
        _forces.Remove(id);
    }
}