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

    public void ToggleThrusterVisual(Direction direction, bool state)
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
