using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const int MAX_MUSIC_VOL = 10;
    private AudioSource _bgm;
    private static float musicTime;
    private static int currentMusicVol = 6;
    public static MusicManager Instance { get; private set; }
    public event EventHandler OnMusicVolumeChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(transform.gameObject);
        _bgm = GetComponent<AudioSource>();
        _bgm.time = musicTime;
    }    
    
    private void Update()
    {
        musicTime = _bgm.time;
    }

    public void UpdateMusicVolume()
    {
        currentMusicVol = (currentMusicVol + 1) % MAX_MUSIC_VOL;
        _bgm.volume = GetNormalizedMusicVolume();
        OnMusicVolumeChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetMusicVolume()
    {
        return currentMusicVol;
    }

    private float GetNormalizedMusicVolume()
    {
        return Mathf.Clamp01((float)currentMusicVol / MAX_MUSIC_VOL);
    }
}
