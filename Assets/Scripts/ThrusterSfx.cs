using System;
using UnityEngine;

public class ThrusterSfx : MonoBehaviour
{
    private Lander _lander;
    [SerializeField]private AudioSource thrusterSfx;

    private void Awake()
    {
        _lander = GetComponent<Lander>();
    }

    private void Start()
    {
        _lander.OnBeforeForce += Lander_OnBeforeForce;
        _lander.OnForceUp += Lander_OnForceUp;
        _lander.OnForceLeft += Lander_OnForceLeft;
        _lander.OnForceRight += Lander_OnForceRight;
        thrusterSfx.Pause();
    }

    private void PlayThrusterSfx()
    {
        
        if (!thrusterSfx.isPlaying)
        {
            thrusterSfx.Play();
        }
    }
    private void PauseThrusterSfx()
    {
        thrusterSfx.Pause();
    }
    
    private void Lander_OnBeforeForce(object sender, EventArgs e)
    {
        PauseThrusterSfx();
    }
    
    private void Lander_OnForceUp(object sender, EventArgs e)
    {
        PlayThrusterSfx();
    }        

        
    private void Lander_OnForceLeft(object sender, EventArgs e)
    {
        PlayThrusterSfx();
    }        
        
    private void Lander_OnForceRight(object sender, EventArgs e)
    {
        PlayThrusterSfx(); 
    }

 }
