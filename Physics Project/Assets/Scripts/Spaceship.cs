using System;
using UnityEngine;

public class Spaceship : PhysicsObject
{
    [SerializeField] private float thrusterSpeed;
    [SerializeField] private float thrusterStopSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float fuelTank;
    [SerializeField] private LineRenderer lineRenderer;
    private Force _thrustersForce = new Force(Vector3.zero);
    private Vector3 _currentThrustersValue;
    private Vector3 _velocity;

    private SpaceshipVisual _visual;
    private bool _fuelDepleted;

    public float FuelTank => fuelTank;
    public bool ThrustersActive { get; private set; }
    public float CurrentFuel { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        _visual ??= GetComponent<SpaceshipVisual>();
    }

    protected override void Start()
    {
        base.Start();
        Velocity.AddForce(_thrustersForce);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CurrentFuel = fuelTank;
    }

    private void Update()
    {
        HandleThrusters();
    }
    
    private void HandleThrusters()
    {
        RotationalThrusters();
        if (CurrentFuel <= -0.1f)
        {
            if(!_fuelDepleted) DepleteFuel();
            return;
        }
        DirectionalThrusters();
        if (Input.GetKey(KeyCode.F))
        {
            _thrustersForce.Value =  (Mass * thrusterStopSpeed * -Speed) / Time.fixedDeltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            _thrustersForce.Value = Vector3.zero;
        }

        if (ThrustersActive)
        {
            CurrentFuel -= Time.deltaTime;
        }
    }

    private void DepleteFuel()
    {
        //add sound for fuel exaust
        _visual.ToggleAllThrusterVisuals(false);
        _fuelDepleted = true;
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

        if (forwardAxis != 0 || rightAxis != 0 || upAxis != 0) ThrustersActive = true;
        else ThrustersActive = false;
        _thrustersForce.Value = thrusterSpeed * (transform.forward * forwardAxis + transform.right * rightAxis + transform.up * upAxis);
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,Speed);
    }
}
