using System;
using UnityEngine;
using UnityEngine.UI;

public class CargoProgress : MonoBehaviour
{
    [SerializeField] private Image cargoProgress;
    private CargoArea cargoArea;

    private void Awake()
    {
        cargoArea = GetComponent<CargoArea>();
    }
     private void Start()
    {
        cargoProgress.fillAmount = 0;
        cargoArea.OnCargoAreaEnter += OnEnter_CargoArea;
        cargoArea.OnCargoAreaExit += OnExit_CargoArea;
    }

    private void OnExit_CargoArea(object sender, EventArgs e)
    {
        cargoProgress.fillAmount = cargoArea.GetInteractTimerNormalized();
    }

    private void OnEnter_CargoArea(object sender, EventArgs e)
    {
        cargoProgress.fillAmount = cargoArea.GetInteractTimerNormalized();
    }
}
