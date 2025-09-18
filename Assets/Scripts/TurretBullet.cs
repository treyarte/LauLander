using System;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool isDestroyed = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);
    }

    public void Setup(Vector2 moveDirection)
    {
        float speed = 7f;
        _rb.linearVelocity = moveDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDestroyed = true;
    }

    private void LateUpdate()
    {
        if (isDestroyed)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
