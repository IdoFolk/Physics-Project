using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private List<CheckPoint> _checkPoints;
    
    private void Start()
    {
        for (int i = 0; i < _checkPoints.Count; i++)
        {
            _checkPoints[i].ID = i;
            _checkPoints[i].OnCheckpointPassed += HandleCheckpointPassed;
            if(i != 0) _checkPoints[i].Hide();
        }
    }
    private void OnDestroy()
    {
        foreach (var checkpoint in _checkPoints)
        {
            checkpoint.OnCheckpointPassed -= HandleCheckpointPassed;
        }
    }
    private void HandleCheckpointPassed(int checkpointID)
    {
        _checkPoints[checkpointID].Hide();
        
        if (_checkPoints[checkpointID].IsEndPoint) 
            EndLevel();
        else 
            _checkPoints[checkpointID+1].Show();
    }

    private void EndLevel()
    {
        Debug.Log("Game Over");
    }
}