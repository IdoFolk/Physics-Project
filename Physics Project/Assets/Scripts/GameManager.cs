using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PanelManager _panelManager;
    [SerializeField] private Animator _menuPanel;
    [SerializeField] private Animator _lookAroundPanel;
    [SerializeField] private Planet _planet;

    private bool _lookAroundMode;

    protected override void Awake()
    {
        base.Awake();
        _camera.GetComponent<FreeCamera>().enabled = false;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_lookAroundMode) ExitLookAround();
            _panelManager.OpenPanel(_menuPanel);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Level 1");
        
    }

    public void OnLevelEnd(int levelID)
    {
        switch (levelID+1)
        {
            case 2:
                SceneManager.LoadScene("Level 2");
                break;
            case 3:
                SceneManager.LoadScene("Level 3");
                break;
            case 4:
                SceneManager.LoadScene("Level 4");
                break;
            case 5:
                SceneManager.LoadScene("Level 4");
                break;
            case 6:
                EndGame();
                break;
        }
    }

    private void EndGame()
    {
        throw new NotImplementedException();
    }

    public void ChangeTime(float value)
    {
        Time.timeScale = value;
    }

    public void EnterLookAround()
    {
        _camera.GetComponent<FreeCamera>().enabled = true;
        _panelManager.CloseCurrent();
        _panelManager.OpenPanel(_lookAroundPanel);
        _lookAroundMode = true;
    }
    private void ExitLookAround()
    {
        _camera.GetComponent<FreeCamera>().enabled = false;
        _lookAroundMode = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}