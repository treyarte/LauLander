using System;
using UnityEngine;

public class LanderVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticles;
    [SerializeField] private ParticleSystem midThrusterParticles;
    [SerializeField] private ParticleSystem rightThrusterParticles;
    [SerializeField] private GameObject gameOverVFX;
    private Lander lander;
    private void Awake()
    {
        lander = GetComponent<Lander>();
        lander.OnForceUp += OnUpForce;
        lander.OnForceLeft += OnLeftForce;
        lander.OnForceRight += OnRightForce;
        lander.OnBeforeForce += OnBeforeForce;
        
        
        HandleThrusterParticles(leftThrusterParticles, false);
        HandleThrusterParticles(midThrusterParticles, false);
        HandleThrusterParticles(rightThrusterParticles, false);
     }

    private void Start()
    {
        lander.OnLanded += Lander_OnLanded;
    }
    
    private void Lander_OnLanded(object sender, LandingEventArgs e)
    {
        switch (e.Type)
        {
            case LandingType.Crash:
                Instantiate(gameOverVFX, transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
                break;
            case LandingType.Success:
                break;
                
        }

    }

    private void OnLeftForce(object sender, System.EventArgs e)
    {
        HandleThrusterParticles(leftThrusterParticles, true);
        HandleThrusterParticles(midThrusterParticles, false);
        HandleThrusterParticles(rightThrusterParticles, false);
    }

    private void OnUpForce(object sender, System.EventArgs e)
    {
        HandleThrusterParticles(leftThrusterParticles, true);
        HandleThrusterParticles(midThrusterParticles, true);
        HandleThrusterParticles(rightThrusterParticles, true);
    }

    private void OnRightForce(object sender, System.EventArgs e)
    {
        HandleThrusterParticles(leftThrusterParticles, false);
        HandleThrusterParticles(midThrusterParticles, false);
        HandleThrusterParticles(rightThrusterParticles, true);
    }
    
    private void OnBeforeForce(object sender, System.EventArgs e)
    {
        HandleThrusterParticles(leftThrusterParticles, false);
        HandleThrusterParticles(midThrusterParticles, false);
        HandleThrusterParticles(rightThrusterParticles, false);
    }
    
    private void HandleThrusterParticles(ParticleSystem particles, bool isEnabled)
    {
        var emissionMod = particles.emission;
        emissionMod.enabled = isEnabled;
    }
    
}
