using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem forwardThrusterParticleSystem;
    [SerializeField] private ParticleSystem backwardThrusterParticleSystem;
    [SerializeField] private ParticleSystem upThrusterParticleSystem;
    [SerializeField] private ParticleSystem downThrusterParticleSystem;

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
    private void ToggleThrusterVisual(Direction direction, bool state)
    {
        switch (direction)
        {
            case Direction.Up:
                upThrusterParticleSystem.gameObject.SetActive(state);
                break;
            case Direction.Down:
                downThrusterParticleSystem.gameObject.SetActive(state);
                break;
            case Direction.Left:
                leftThrusterParticleSystem.gameObject.SetActive(state);
                break;
            case Direction.Right:
                rightThrusterParticleSystem.gameObject.SetActive(state);
                break;
            case Direction.Forward:
                forwardThrusterParticleSystem.gameObject.SetActive(state);
                break;
            case Direction.Backward:
                backwardThrusterParticleSystem.gameObject.SetActive(state);
                break;
        }
    }

    private void ToggleAllThrusterVisuals(bool state)
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
