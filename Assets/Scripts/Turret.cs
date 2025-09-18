using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform TurretHead;
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private TurretBullet TurretBulletPrefab;
    private float shootTimer;
    private void Update()
    {
        if (!IsLanderInRange())
        {
            return;
        }
        
        Vector2 direction = (Lander.Instance.transform.position - TurretHead.position).normalized;
        TurretHead.right = direction;
        
        shootTimer -=  Time.deltaTime;

        if (shootTimer < 0f)
        {
            float shootTimerMax = 4.5f;
            shootTimer = shootTimerMax;
            
            TurretBullet turretBullet = Instantiate(TurretBulletPrefab, TurretHead.position, Quaternion.identity);
            turretBullet.Setup(direction);
        }
            
            
    }

    private bool IsLanderInRange()
    {
        float range = 20f;
        return Vector2.Distance(Lander.Instance.transform.position, transform.position) < range;
    }
}
