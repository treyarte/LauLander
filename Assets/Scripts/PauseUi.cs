using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUi : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button musicVolButton;
    [SerializeField] private Button soundVolButton;
    [SerializeField] private TextMeshProUGUI musicVolButtonText;
    [SerializeField] private TextMeshProUGUI soundVolButtonText;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnPauseGame();
        });
        
        soundVolButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.UpdateSoundVolume();
            SetSoundVolumeText();
        });        
        
        musicVolButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.UpdateMusicVolume();
            SetMusicVolumeText();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        SetSoundVolumeText();
        SetMusicVolumeText();
        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void SetSoundVolumeText()
    {
        soundVolButtonText.text = "Sound " + SoundManager.Instance.GetCurrentSoundVol();
    }   
    private void SetMusicVolumeText()
    {
        musicVolButtonText.text = "Music " + MusicManager.Instance.GetMusicVolume();
    }
}
