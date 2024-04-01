using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            Time.timeScale += 0.25f;
            Debug.Log($"Time: {Time.timeScale}");
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            Time.timeScale -= 0.25f;
            Debug.Log($"Time: {Time.timeScale}");
        }
    }
}



