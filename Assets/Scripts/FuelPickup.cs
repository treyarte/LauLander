using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    [SerializeField] private float fuelAmount = 10f;
    public float FuelAmount => fuelAmount;

    public void DestroySelf()
    {
        Destroy(gameObject); 
    }
}
