using System;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneVisuals : MonoBehaviour
{
    [SerializeField] private List<Transform> windDirections;
    private WindZone windZone;

    private void Awake()
    {
        windZone = GetComponent<WindZone>();
    }

    private void Start()
    {
        foreach (Transform windDirection in windDirections)
        {
            windDirection.right = windZone.GetForce();
        }
    }
}
