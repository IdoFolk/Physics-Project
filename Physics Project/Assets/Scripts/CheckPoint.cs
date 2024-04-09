using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public event Action<int> OnCheckpointPassed;
    public int ID { get; set; }

    public bool IsEndPoint => _isEndPoint;

    [SerializeField] private bool _isEndPoint;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.TryGetComponent<Spaceship>(out var spaceship))
        {
            OnCheckpointPassed?.Invoke(ID);
        }
    }
}
