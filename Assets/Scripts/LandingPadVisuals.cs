using TMPro;
using UnityEngine;

public class LandingPadVisuals : MonoBehaviour
{
    void Awake()
    {
        var textMeshPro = GetComponentInChildren<TextMeshPro>();
        TryGetComponent<LandingPad>(out var landingPad);
        
        if (!textMeshPro || !landingPad) {return;}
        
        float scoreMultiplier = landingPad.GetScoreMultiplier();
        textMeshPro.text = "x" + scoreMultiplier;

    }
}
