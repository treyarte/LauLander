using System;
using UnityEngine;

public class CargoArea : MonoBehaviour
{
    [SerializeField] private float interactTimerMax = 2f;
    [SerializeField] private InteractType interactType;
    private float _interactiveTimer;

    public event EventHandler OnCargoAreaEnter;
    public event EventHandler OnCargoAreaExit;
    public event EventHandler OnCargoDropOff;
    public event EventHandler OnCargoPickUp;
    
    public enum InteractType
    {
        Pickup,
        DropOff
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lander lander))
        {
            _interactiveTimer += Time.deltaTime;
            OnCargoAreaEnter?.Invoke(this, EventArgs.Empty);
            if (_interactiveTimer > interactTimerMax)
            {
                switch (interactType)
                {
                    case InteractType.Pickup:
                        lander.LoadCargo();
                        OnCargoPickUp?.Invoke(this, EventArgs.Empty);
                        break;
                    case InteractType.DropOff:
                        lander.DropOffCargo();
                        OnCargoDropOff?.Invoke(this, EventArgs.Empty);
                        break;
                }
                DestroySelf();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interactiveTimer = 0f;
        OnCargoAreaExit?.Invoke(this, EventArgs.Empty);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public float GetInteractTimerNormalized()
    {
        return _interactiveTimer/interactTimerMax;
    }
}