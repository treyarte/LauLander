using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSfx;
    [SerializeField] private AudioClip fuelPickupSfx;
    [SerializeField] private AudioClip crashLandingSfx;
    [SerializeField] private AudioClip successLandingSfx;
    

    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_CoinPickup;
        Lander.Instance.OnFuelPickup += Lander_OnFuelPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, LandingEventArgs e)
    {
        if (Camera.main == null) { return;}
        
        switch (e.Type)
        {
            case LandingType.Success:
                AudioSource.PlayClipAtPoint(successLandingSfx, Camera.main.transform.position);
                break;
            default:
                AudioSource.PlayClipAtPoint(crashLandingSfx, Camera.main.transform.position);
                break;
        }
    }

    void Lander_OnFuelPickup(object sender, float e)
    {
        if (Camera.main == null) {return;}
        AudioSource.PlayClipAtPoint(fuelPickupSfx, Camera.main.transform.position);
    }

    void Lander_CoinPickup(object sender, int e)
    {
        if (Camera.main == null) {return;}
            
        AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
    }
}
