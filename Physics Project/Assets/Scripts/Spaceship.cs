using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spaceship : PhysicsObject
{
    [SerializeField] private float thrusterSpeed;
    [SerializeField] private float thrusterStopSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private SpaceshipVisual spaceshipVisual;
    private Force _thrustersForce = new Force(Vector3.zero);
    private Vector3 _currentThrustersValue;
    private bool _thrustersActive;
    private Vector3 _velocity;
    

    public override void Start()
    {
        base.Start();
        _forces.Add(_thrustersForce);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleThrusters();
        HandleCannons();
    }

    private void HandleCannons()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //shoot projectile
        }
    }

    private void HandleThrusters()
    {
        DirectionalThrusters();
        RotationalThrusters();
        if (Input.GetKey(KeyCode.F))
        {
            _thrustersForce.Value =  (Mass * thrusterStopSpeed * -Speed) / Time.fixedDeltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            _thrustersForce.Value = Vector3.zero;
        }
    }
    private void RotationalThrusters()
    {
        var xAxis = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        var yAxis = Input.GetAxis("Mouse Y")* rotateSpeed * Time.deltaTime;
        float zAxis = 0;
        
        if (Input.GetKey(KeyCode.Q)) zAxis += 1;
        
        if (Input.GetKey(KeyCode.E)) zAxis -= 1;
        

        zAxis *= rotateSpeed * Time.deltaTime;
        
        transform.Rotate(-yAxis, xAxis,zAxis);
    }
    
    private void DirectionalThrusters()
    {

        var rightAxis = Input.GetAxis("Horizontal");
        var forwardAxis = Input.GetAxis("Vertical");
        var upAxis = Input.GetAxis("Depth");

        _thrustersForce.Value = thrusterSpeed * (transform.forward * forwardAxis + transform.right * rightAxis + transform.up * upAxis);
        
        
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     _thrustersForce.Value = transform.forward * speed;
        // }
        //
        // if (Input.GetKey(KeyCode.S))
        // {
        //     _thrustersForce.Value = -transform.forward * speed;
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //     _thrustersForce.Value = -transform.right * speed;
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     _thrustersForce.Value = transform.right * speed;
        // }
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     _thrustersForce.Value = transform.up * speed;
        // }
        // if (Input.GetKey(KeyCode.LeftControl))
        // {
        //     _thrustersForce.Value = -transform.up * speed;
        // }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,_gravityForce.Value);
    }
}
