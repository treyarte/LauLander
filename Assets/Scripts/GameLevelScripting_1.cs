using System;
using UnityEngine;

public class GameLevelScripting_1 : MonoBehaviour
{
    [SerializeField] private CargoArea cargoArea;

    [SerializeField] private int cargoScore = 1000;
    
    private void Start()
    {
        cargoArea.OnCargoDropOff += CargoArea_OnDropOff;
    }

    private void CargoArea_OnDropOff(object sender, EventArgs e)
    {
        GameManager.Instance.UpdateScore(cargoScore);
    }
}
