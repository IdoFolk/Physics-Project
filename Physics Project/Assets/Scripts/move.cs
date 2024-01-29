using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    [SerializeField] private MoveType moveType;
    [SerializeField] private float fixedSpeed;
    [SerializeField] private float fixedVelocity;
    [SerializeField] private AnimationCurve dynamicVelocity;
    [SerializeField] private float dynamicVelocityAnimationSpeed;

    [SerializeField]private float speed;
    [SerializeField]private float velocity;
    private float _animationCurveTime;

    private void Start()
    {
        switch (moveType)
        {
            case MoveType.FixedSpeed:
                speed = fixedSpeed;
                velocity = 0;
                break;
            case MoveType.FixedVelocity:
                speed = fixedSpeed;
                velocity = fixedVelocity;
                break;
            case MoveType.DynamicVelocity:
                speed = fixedSpeed;
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (moveType)
        {
            case MoveType.FixedVelocity:
                speed += velocity * Time.fixedDeltaTime;
                break;
            case MoveType.DynamicVelocity:
                _animationCurveTime += Time.fixedDeltaTime * dynamicVelocityAnimationSpeed;
                if (_animationCurveTime >= 1) _animationCurveTime = 0;
                velocity = dynamicVelocity.Evaluate(_animationCurveTime) * fixedVelocity;
                speed += velocity * Time.fixedDeltaTime;
                break;
        }

        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed);
    }
}

public enum MoveType
{
    FixedSpeed,
    FixedVelocity,
    DynamicVelocity
}