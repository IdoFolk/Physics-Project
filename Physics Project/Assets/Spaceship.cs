using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : PhysicsObject
{
    [SerializeField] private float speed;
    private Vector3 _thrustersForce = Vector3.zero;

    public override void Start()
    {
        base.Start();
        _forces.Add(_thrustersForce);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _thrustersForce = Vector3.forward * speed;
        }
    }
}
