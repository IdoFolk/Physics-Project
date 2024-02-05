using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : PhysicsObject
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private SpaceshipVisual spaceshipVisual;
    private Force _thrustersForce = new Force(Vector3.zero);
    

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

        // if (Physics.BoxCast(transform.position, transform.localScale / 2, Vector3.forward,out var hit))
        // {
        //     Debug.Log("colliding with:" + hit.collider.name);
        // }
    }

    private void HandleThrusters()
    {
        DirectionalThrusters();
        ThrustersVisuals();
        RotationalThrusters();
        if (Input.GetKey(KeyCode.F))
        {
            
            _thrustersForce.Value += -Velocity * speed;
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

    private void ThrustersVisuals()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Forward,true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Forward,false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Backward,true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Backward,false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Left,true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Left,false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Right,true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Right,false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Up,true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Up,false);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Down,true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            spaceshipVisual.ToggleThrusterVisual(Direction.Down,false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            spaceshipVisual.ToggleAllThrusterVisuals(true);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            spaceshipVisual.ToggleAllThrusterVisuals(false);
        }
    }

    private void DirectionalThrusters()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _thrustersForce.Value += Vector3.forward * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _thrustersForce.Value += Vector3.back * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _thrustersForce.Value += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _thrustersForce.Value += Vector3.right * speed;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _thrustersForce.Value += Vector3.up * speed;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _thrustersForce.Value += Vector3.down * speed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,_gravityForce.Value);
    }
}
