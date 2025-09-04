using System;
using UnityEngine;

public class CargoRopeCrate : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.TryGetComponent(out Lander lander))
        {
            Lander.Instance.CrashLander();
        }
    }
}
