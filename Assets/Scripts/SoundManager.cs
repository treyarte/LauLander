using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSfx;
    [SerializeField] private AudioClip fuelPickupSfx;
    [SerializeField] private AudioClip crashLandingSfx;
    [SerializeField] private AudioClip successLandingSfx;
    private const int MAX_SOUND_VOL = 10;
    private static int currentSoundVol = 6; 
    public static SoundManager Instance {get; private set;}
    public event EventHandler OnSoundVolumeChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_CoinPickup;
        Lander.Instance.OnFuelPickup += Lander_OnFuelPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, LandingEventArgs e)
    {
        switch (e.Type)
        {
            case LandingType.Success:
                PlaySound(successLandingSfx);        
                break;
            default:
                PlaySound(crashLandingSfx);
                break;
        }
    }

    private void Lander_OnFuelPickup(object sender, float e)
    {
        PlaySound(fuelPickupSfx);
    }

    private void Lander_CoinPickup(object sender, int e)
    {
        PlaySound(coinPickupSfx);
    }

    private void PlaySound(AudioClip clip, Vector3? position = null)
    {
        Vector3 pos = Camera.main != null ?  Camera.main.transform.position : Vector3.zero;
        
        if (position != null)
        {
            pos = position.Value;
        }
        
        AudioSource.PlayClipAtPoint(clip, pos, GetNormalizedSoundVol());
    }

    public int GetCurrentSoundVol()
    {
        return currentSoundVol;
    }    
    
    public float GetNormalizedSoundVol()
    {
        return Mathf.Clamp01((float)currentSoundVol/ MAX_SOUND_VOL);
    }
    public void UpdateSoundVolume()
    {
        currentSoundVol = (currentSoundVol + 1) % MAX_SOUND_VOL;
        OnSoundVolumeChanged?.Invoke(this, EventArgs.Empty);
    }
}
