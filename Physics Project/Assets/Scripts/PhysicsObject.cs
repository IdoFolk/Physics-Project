using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public event Action<PhysicsObject> OnCollisionEnter;
    public event Action<PhysicsObject> OnCollision;
    public event Action<PhysicsObject> OnCollisionExit;
    // G - Gravitational constant 
    private static readonly float G = 6.674f * Mathf.Pow(10, -11);
    public SumForces Velocity { get; private set; } = new(0);

    public SumForces GravityForces { get; private set; } = new(1);
    public Collider Collider { get; private set; }
    public Vector3 Speed { get; private set; }
    public float Mass => _mass;
    public float Bounciness => _bounciness;
    public PhysicsObject PlanetOrbit => _planetOrbit;
    public Vector3 Position => transform.position;

    [SerializeField] private float _mass;
    [SerializeField, Range(0, 2)] private float _bounciness;
    [SerializeField] private bool _isMoveable;
    [SerializeField] private bool _usingGravity;
    [SerializeField] private PhysicsObject _planetOrbit;
    [SerializeField] private bool _onlyOrbitAroundPlanet;
    [SerializeField] private bool _hasCollider;
    [SerializeField] private ColliderConfig _colliderConfig;
    [SerializeField, Range(0, 0.1f)] private float _penetrationTolerance;


    private bool _isColliding;
    private Vector3 _lastPos;


    protected virtual void Awake()
    {
        if (_hasCollider) Collider = new Collider(_colliderConfig, transform);
    }

    protected virtual void Start()
    {
        _lastPos = Position;
        if (_usingGravity) Velocity.AddForce(GravityForces);
        if(_planetOrbit != null) SetInitialOrbitalSpeed(_planetOrbit);
    }


    protected virtual void FixedUpdate()
    {
        Speed += (Velocity.Value / _mass) * Time.fixedDeltaTime;
        transform.position += Speed * Time.fixedDeltaTime;
    }
    public virtual void DestroyObject(){}

    public void ApplyGravityForce(int objectID, Vector3 force)
    {
        if (!_usingGravity) return;
        if (_onlyOrbitAroundPlanet)
        {
            if(ReferenceEquals(_planetOrbit,null)) return;
            if(_planetOrbit.GetInstanceID() != objectID) return;
        }
        GravityForces.ChangeForce(objectID, force);
    }

    public void CheckCollision(PhysicsObject otherPhysicsObject)
    {
        var distance = Vector3.Distance(Position, otherPhysicsObject.Position);
        if (!(distance < (Collider.Radius*2 + otherPhysicsObject.Collider.Radius*2) * 2)) return;

        if (Collider.CheckCollision(otherPhysicsObject.Collider))
        {
            if (!_isColliding)
            {
                OnCollisionEnter?.Invoke(otherPhysicsObject);
                _isColliding = true;
            }
            OnCollision?.Invoke(otherPhysicsObject);
            // Calculate collision normal
            Vector3 collisionNormal = (Position - otherPhysicsObject.Position).normalized;
            if (_isMoveable) ApplyNormalForce(collisionNormal);

            // Resolve penetration by adjusting positions
            float penetrationDepth = (Collider.Radius + otherPhysicsObject.Collider.Radius) - distance;
            if (penetrationDepth > _penetrationTolerance)
            {
                Vector3 correctionVector = collisionNormal * penetrationDepth;
                if (_isMoveable) transform.position += correctionVector * 0.5f / _mass; // Apply half of the correction to each object
                if (otherPhysicsObject._isMoveable) otherPhysicsObject.transform.position -= (correctionVector * 0.5f) / _mass;
            }
        }
        else
        {
            OnCollisionExit?.Invoke(otherPhysicsObject);
            _isColliding = false;
        }
    }

    protected virtual void ApplyNormalForce(Vector3 collisionNormal)
    {
        Speed = Vector3.Reflect(Speed, collisionNormal) * _bounciness;
    }

    private void SetInitialOrbitalSpeed(PhysicsObject planet)
    {
        // Calculate the distance between the satellite and planet
        float r = Vector3.Distance(planet.Position, Position);

        // Calculate the orbital speed needed
        float v = Mathf.Sqrt(PhysicsManager.Instance.GravityScale * planet.Mass / r);

        // Get direction from satellite to planet
        Vector3 direction = (planet.Position - Position).normalized;

        // Rotate direction 90 degrees around Y axis to obtain a tangential direction (perpendicular to original direction):
        Vector3 tangentialDirection = Quaternion.Euler(0, 90, 0) * direction;

        // Set initial speed of satellite
        Speed = tangentialDirection * v;
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