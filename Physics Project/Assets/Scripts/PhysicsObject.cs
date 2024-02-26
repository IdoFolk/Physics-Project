using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float Mass => _mass;

    private float _test;
    public float Test
    {
        get => _test;
        set => _test = value;
    }
    public float Speed => _speed;
    public Vector3 Position => transform.position;
    [SerializeField] private float _mass;
    [SerializeField] private bool _usingGravity;
    [SerializeField] private bool _hasCollider;
    [SerializeField] private ColliderConfig _colliderConfig;
    

    private bool _isColliding;
    protected List<Force> _forces = new List<Force>();
    protected Force _gravityForce = new Force(Vector3.zero);
    private Force _angularForce = new Force(Vector3.zero);
    private Collider _collider;
    private float _speed;


    private Vector3 _lastPos;
    private Vector3 _velocity;
    public Vector3 ForcesSum
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

    private void Awake()
    {
        if (_hasCollider) _collider = new Collider(_colliderConfig);
    }

    public virtual void Start()
    {
        if (_usingGravity) _forces.Add(_gravityForce);
        //if (_usingGravity) _forces.Add(_angularForce);
    }


    protected virtual void FixedUpdate()
    {
        _velocity += (ForcesSum / _mass) * Time.fixedDeltaTime;
        transform.position += _velocity * Time.fixedDeltaTime;
        
    }

    public void ApplyGravityForce(Vector3 force)
    {
        if (!_usingGravity) return;
        if (_isColliding)
        {
            _gravityForce.Value = Vector3.zero;
            return;
        }
        _gravityForce.Value = force;
        
    }
    public void ApplyAngularForce(float radius, Vector3 direction)
    {
        if(_isColliding) return;
        var force = (Speed * Speed * _mass / radius) * direction;
        _angularForce.Value = force;
    }

    private void CalculateSpeed()
    {
        var distance = Vector3.Distance(_lastPos, Position);
        _speed = distance;
        Debug.Log(_speed);
        _lastPos = Position;
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
