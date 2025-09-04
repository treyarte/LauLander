using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LandingEventArgs
{
    public int Score;
    public LandingType Type;
    public float LandingAngle;
    public float LandingSpeed;
    public float ScoreMultiplier;
}

public enum LandingType {
    Crash = 0,
    Success = 1,
}

public enum LanderState
{
    WaitingToStart = 0,
    Normal,
    GameOver,
}

public class Lander : MonoBehaviour
{
    private Rigidbody2D landerBody2D;
    private LanderState landerState = LanderState.WaitingToStart;
    private const float  LANDER_GRAVITY = 0.7f;
    private Transform cargoRopeInstance;
    [SerializeField] private Transform cargoRopePrefab;
    [SerializeField] private float forceUp = 700f;
    [SerializeField] private float torqueLeft = +100f;
    [SerializeField] private float torqueRight = -100f;
    [SerializeField] private float maxFuel = 10f;
    [SerializeField] private float fuel;
    public static Lander Instance {get; private set;}
    public event EventHandler OnForceUp;
    public event EventHandler OnForceLeft;
    public event EventHandler OnForceRight;
    public event EventHandler OnBeforeForce;
    public event EventHandler<LandingEventArgs> OnLanded;
    public event EventHandler<LanderState> OnStateChanged;
    public event EventHandler<int> OnCoinPickup;
    public event EventHandler<float> OnFuelPickup;
    


    
    private void Awake()
    {
        Instance = this;
        landerBody2D = GetComponent<Rigidbody2D>();
        landerBody2D.gravityScale = 0;
        fuel = maxFuel;
    }

    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        switch (landerState)
        {
            case LanderState.WaitingToStart:
                if (GameInput.Instance.GetIsLanderUpPressed() ||
                    GameInput.Instance.GetIsLanderLeftPressed() ||
                    GameInput.Instance.GetIsLanderRightPressed() ||
                    GameInput.Instance.GetMovementVector2D() != Vector2.zero)
                {
                    landerBody2D.gravityScale = LANDER_GRAVITY;
                    HandleStateChange(LanderState.Normal);
                }
                break;
            case LanderState.Normal:
                if (GameInput.Instance.GetIsLanderUpPressed()||
                    GameInput.Instance.GetIsLanderLeftPressed()||
                    GameInput.Instance.GetIsLanderRightPressed() ||
                    GameInput.Instance.GetMovementVector2D() != Vector2.zero)
                {
                    if (fuel <= 0)
                    {
                        //No fuel
                        return;
                    }
                    
                    ConsumeFuel();

                    float gamepadDeadZone = 0.4f;
                    if (GameInput.Instance.GetIsLanderUpPressed() || GameInput.Instance.GetMovementVector2D().y > gamepadDeadZone)
                    {
                        landerBody2D.AddForce(transform.up * (forceUp * Time.deltaTime));
                        OnForceUp?.Invoke(this, EventArgs.Empty);
                    }         
                    if (GameInput.Instance.GetIsLanderLeftPressed() || GameInput.Instance.GetMovementVector2D().x < -gamepadDeadZone)
                    {
                        landerBody2D.AddTorque(torqueLeft * Time.deltaTime);
                        OnForceLeft?.Invoke(this, EventArgs.Empty);
                    }         
                    if (GameInput.Instance.GetIsLanderRightPressed() || GameInput.Instance.GetMovementVector2D().x > gamepadDeadZone)
                    {
                        landerBody2D.AddTorque(torqueRight * Time.deltaTime);
                        OnForceRight?.Invoke(this, EventArgs.Empty);
                    }  
                }
                break;
            default:
                break;
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent(out LandingPad landingPad);

        if (!landingPad)
        {
            Debug.Log("Crashed on Terrain");
            OnLanded?.Invoke(this, new LandingEventArgs {
                Score = 0,
                Type = LandingType.Crash,
                LandingAngle = 0f,
                ScoreMultiplier = 0,
                LandingSpeed = 0f
            });
            HandleStateChange(LanderState.GameOver);
            return;
        }
        
        float softLandingVelocityMagnitude = 4f;
        float currentVelocityMagnitude = collision.relativeVelocity.magnitude;

        if (currentVelocityMagnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Too fast; Crash landing!");
            OnLanded?.Invoke(this, new LandingEventArgs {
                Score = 0,
                Type = LandingType.Crash,
                LandingAngle = 0f,
                ScoreMultiplier = landingPad.GetScoreMultiplier(),
                LandingSpeed = currentVelocityMagnitude
            });
            HandleStateChange(LanderState.GameOver);
            return;
        }

        float dotValue = Vector2.Dot(Vector2.up, transform.up);
        float minDotValue = .90f;

        if (dotValue < minDotValue)
        {
            Debug.Log("Landed on a steep angle; Crash Landing!");
            OnLanded?.Invoke(this, new LandingEventArgs {
                Score = 0,
                Type = LandingType.Crash,
                LandingAngle = dotValue,
                ScoreMultiplier = landingPad.GetScoreMultiplier(),
                LandingSpeed = currentVelocityMagnitude
            });
            HandleStateChange(LanderState.GameOver);
            return;
        }
        
        Debug.Log("Successful landing!");
        float angleMaxScore = 100f;
        float angleScoreMulitplier = 10f;
        float landingAngleScore = angleMaxScore - Math.Abs(dotValue - 1f) * angleScoreMulitplier * angleMaxScore;
        
        
        float speedMaxScore = 100f;
        float landingSpeedScore = (softLandingVelocityMagnitude - currentVelocityMagnitude) * speedMaxScore;
        
        Debug.Log("Landing Angle: " + landingAngleScore);
        Debug.Log("Landing Speed: " + landingSpeedScore);
        
        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.GetScoreMultiplier());
        OnLanded?.Invoke(this, new LandingEventArgs {
                Score = score,
                Type = LandingType.Success,
                LandingAngle = dotValue,
                ScoreMultiplier = landingPad.GetScoreMultiplier(),
                LandingSpeed = currentVelocityMagnitude
            });
        HandleStateChange(LanderState.GameOver);
    }
    
    private void OnTriggerEnter2D(Collider2D triggerCollider2D)
    {
        if (triggerCollider2D.TryGetComponent(out FuelPickup fuelPickup))
        {
            float fuelAmount = fuelPickup.FuelAmount;
            float totalFuel = (fuelAmount + fuel);
            
            fuel = totalFuel > maxFuel ? maxFuel : totalFuel;
            OnFuelPickup?.Invoke(this, fuel);
            fuelPickup.DestroySelf();
        }
        
        if (triggerCollider2D.TryGetComponent(out Coin coin))
        {
            OnCoinPickup?.Invoke(this, coin.Value);
            coin.DestroySelf();
        }
        
    }

    private void HandleStateChange(LanderState landerState)
    {
        this.landerState = landerState;
        OnStateChanged?.Invoke(this, landerState);
    }

    private void ConsumeFuel(float fuelAmount = -1f)
    {
        fuel += fuelAmount * Time.deltaTime;
    }

    public float GetNormalizedFuel()
    {
        return fuel/maxFuel;
    }

    public float GetSpeedX()
    {
        return landerBody2D.linearVelocityX;
    }
    
    public float GetSpeedY()
    {
        return landerBody2D.linearVelocityY;
    }

    public void LoadCargo()
    { 
        cargoRopeInstance = Instantiate(cargoRopePrefab, transform);
    }

    public void DropOffCargo()
    {
        Destroy(cargoRopeInstance.gameObject);
    }

    public void CrashLander()
    {
        OnLanded?.Invoke(this, new LandingEventArgs {
            Score = 0,
            Type = LandingType.Crash,
            LandingAngle = 0f,
            ScoreMultiplier = 0,
            LandingSpeed = 0f
        });
    }
    
    
    
    
}
