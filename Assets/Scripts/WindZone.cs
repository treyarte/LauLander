using System;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    
    private void OnTriggerStay2D(Collider2D colliderObj)
    {
        if (colliderObj.TryGetComponent(out Lander lander))
        {
            lander.AddForce(force);
        }
    }

    public Vector2 GetForce()
    {
        return force;
    }
}
