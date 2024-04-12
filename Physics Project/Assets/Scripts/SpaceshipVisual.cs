using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceshipVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem forwardThrusterParticleSystem;
    [SerializeField] private ParticleSystem backwardThrusterParticleSystem;
    [SerializeField] private ParticleSystem upThrusterParticleSystem;
    [SerializeField] private ParticleSystem downThrusterParticleSystem;
    [SerializeField] private AudioSource _audioSource;
    private Spaceship _owner;
    private bool[] _thrustersActive = new bool[6];

    private void Awake()
    {
        _owner ??= GetComponent<Spaceship>();
    }

    private void Start()
    {
        ToggleAllThrusterVisuals(false);
    }

    private void Update()
    {
        ThrustersVisuals();
    }

    private void ThrustersVisuals()
    {
        if(_owner.CurrentFuel <= 0) return;
        if(Input.GetKeyDown(KeyCode.W))
        {
            ToggleThrusterVisual(Direction.Forward,true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            ToggleThrusterVisual(Direction.Forward,false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleThrusterVisual(Direction.Backward,true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            ToggleThrusterVisual(Direction.Backward,false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ToggleThrusterVisual(Direction.Left,true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            ToggleThrusterVisual(Direction.Left,false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ToggleThrusterVisual(Direction.Right,true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            ToggleThrusterVisual(Direction.Right,false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleThrusterVisual(Direction.Up,true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ToggleThrusterVisual(Direction.Up,false);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleThrusterVisual(Direction.Down,true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ToggleThrusterVisual(Direction.Down,false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleAllThrusterVisuals(true);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            ToggleAllThrusterVisuals(false);
        }
    }
    public void ToggleThrusterVisual(Direction direction, bool state)
    {
        switch (direction)
        {
            case Direction.Up:
                upThrusterParticleSystem.gameObject.SetActive(state);
                _thrustersActive[0] = state;
                break;
            case Direction.Down:
                downThrusterParticleSystem.gameObject.SetActive(state);
                _thrustersActive[1] = state;
                break;
            case Direction.Left:
                leftThrusterParticleSystem.gameObject.SetActive(state);
                _thrustersActive[2] = state;
                break;
            case Direction.Right:
                rightThrusterParticleSystem.gameObject.SetActive(state);
                _thrustersActive[3] = state;
                break;
            case Direction.Forward:
                forwardThrusterParticleSystem.gameObject.SetActive(state);
                _thrustersActive[4] = state;
                break;
            case Direction.Backward:
                backwardThrusterParticleSystem.gameObject.SetActive(state);
                _thrustersActive[5] = state;
                break;
        }
        if (_thrustersActive.Contains(true))
        {
            if(!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else
        {
            if(_audioSource.isPlaying)
                _audioSource.Stop();
        }
    }

    public void ToggleAllThrusterVisuals(bool state)
    {
        upThrusterParticleSystem.gameObject.SetActive(state);
        downThrusterParticleSystem.gameObject.SetActive(state);
        rightThrusterParticleSystem.gameObject.SetActive(state);
        leftThrusterParticleSystem.gameObject.SetActive(state);
        forwardThrusterParticleSystem.gameObject.SetActive(state);
        backwardThrusterParticleSystem.gameObject.SetActive(state);
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    Forward,
    Backward
}
