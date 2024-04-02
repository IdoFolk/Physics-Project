using System;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PanelManager _panelManager;
    [SerializeField] private Animator _menuPanel;
    [SerializeField] private Animator _lookAroundPanel;

    private bool _lookAroundMode;

    protected override void Awake()
    {
        base.Awake();
        _camera.GetComponent<FreeCamera>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_lookAroundMode) ExitLookAround();
            _panelManager.OpenPanel(_menuPanel);
        }
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