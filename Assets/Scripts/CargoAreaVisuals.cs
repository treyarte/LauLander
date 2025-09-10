using System;
using UnityEngine;

public class CargoAreaVisuals : MonoBehaviour
{
    [SerializeField] private SpriteRenderer iconSpriteRenderer;
    private CargoArea cargoArea;

    private void Awake()
    {
        cargoArea = GetComponent<CargoArea>();
    }

    private void Start()
    {
        iconSpriteRenderer.sprite = cargoArea.GetCargoSo().sprite;
    }
}
