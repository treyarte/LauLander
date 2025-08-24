using System;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 500;
    public int Value => value;
    public static event Action<int> OnCoinPickup;

    private void OnTriggerEnter2D(Collider2D trigger2D)
    {
        if (trigger2D.TryGetComponent(out Lander _))
        {
            OnCoinPickup?.Invoke(value);
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
