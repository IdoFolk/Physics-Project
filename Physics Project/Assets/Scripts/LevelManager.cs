using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private int _id;
    [SerializeField] private List<CheckPoint> _checkPoints;
    [SerializeField] private Transform _endLevelUI;
    [SerializeField] private TextMeshProUGUI _endLevelUITitle;
    [SerializeField] private TextMeshProUGUI _endLevelUITime;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private AudioSource _audioSource;

    private float _timeOnLevelStart;
    public Spaceship Spaceship { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Spaceship ??= FindObjectOfType<Spaceship>();
    }

    private void Start()
    {
        _timeOnLevelStart = Time.time;
        for (int i = 0; i < _checkPoints.Count; i++)
        {
            _checkPoints[i].ID = i;
            _checkPoints[i].OnCheckpointPassed += HandleCheckpointPassed;
            if(i != 0) _checkPoints[i].Hide();
        }
        _endLevelUI.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        _audioSource.Play();
        
        if (_checkPoints[checkpointID].IsEndPoint) 
            EndLevel(true);
        else 
            _checkPoints[checkpointID+1].Show();
    }

    public void EndLevel(bool win)
    {
        Spaceship.ToggleControls(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _endLevelUI.gameObject.SetActive(true);
        
        double mainGameTimerd = (double)Time.time - _timeOnLevelStart;
        TimeSpan time = TimeSpan.FromSeconds(mainGameTimerd);
        string displayTime = time.ToString(@"mm\:ss");
        _endLevelUITime.text = $"Time: {displayTime}";
        if (win)
        {
            _endLevelUITitle.text = "Level Completed!";
            _nextLevelButton.interactable = true;
        }
        else
        {
            _endLevelUITitle.text = "Level Failed!";
            _nextLevelButton.interactable = false;
        }
    }

    public void OpenMenu() => SceneManager.LoadScene("Menu");
    public void NextLevel() => GameManager.Instance.OnLevelEnd(_id);
}