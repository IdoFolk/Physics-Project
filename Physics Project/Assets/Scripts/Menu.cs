using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PanelManager _panelManager;
    [SerializeField] private Animator _menuPanel;
    [SerializeField] private Animator _lookAroundPanel;

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _musicVolumeSilder;
    [SerializeField] private Slider _sfxVolumeSilder;
    [SerializeField] private Slider _masterVolumeSilder;
    private bool _lookAroundMode;

    private void Awake()
    {
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
    
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1");
    }

    public void OnSettingsOpen()
    {
        if(_mixer.GetFloat("MusicVolume",out var musicValue))
        {
            _musicVolumeSilder.value = musicValue;
        }
        if(_mixer.GetFloat("MasterVolume",out var masterValue))
        {
            _masterVolumeSilder.value = masterValue;
        }
        if(_mixer.GetFloat("SfxVolume",out var sfxValue))
        {
            _sfxVolumeSilder.value = sfxValue;
        }
    }

    public void ChangeMusicVolume(float value) =>_mixer.SetFloat("MusicVolume", value);
    public void ChangeSfxVolume(float value) =>_mixer.SetFloat("SfxVolume", value);
    public void ChangeMasterVolume(float value) =>_mixer.SetFloat("MasterVolume", value);
    public void ChangeTime(float value)
    {
        Time.timeScale = value;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
