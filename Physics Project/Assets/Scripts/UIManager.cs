using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _fuelHud;
    private Spaceship _spaceship;
    private void Awake()
    {
        _spaceship ??= FindObjectOfType<Spaceship>();
        _fuelHud.maxValue = _spaceship.FuelTank;
        _fuelHud.value = _spaceship.FuelTank;
    }

    private void Update()
    {
        if (_spaceship.ThrustersActive)
        {
            if(_spaceship.CurrentFuel >= 0) _fuelHud.value = _spaceship.CurrentFuel;
        }
    }
}
