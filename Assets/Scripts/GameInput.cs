using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
   public static GameInput Instance { get; private set; }
   private InputActions inputActions;
   public event EventHandler OnMenuButtonPress;
   private void Awake()
   {
      Instance = this;
      inputActions = new InputActions();
      inputActions.Enable();
      inputActions.Lander.Menu.performed += OnMenu_Performed;
   }

   private void OnMenu_Performed(InputAction.CallbackContext obj)
   {
      OnMenuButtonPress?.Invoke(this, EventArgs.Empty);
   }
   
   private void OnDestroy()
   {
      inputActions.Disable();
   }

   public bool GetIsLanderUpPressed()
   {
      return inputActions.Lander.LanderUp.IsPressed();
   }   
   
   public bool GetIsLanderLeftPressed()
   {
      return inputActions.Lander.LanderLeft.IsPressed();
   }
   
   public bool GetIsLanderRightPressed()
   {
      return inputActions.Lander.LanderRIght.IsPressed();
   }

   public Vector2 GetMovementVector2D()
   {
      return inputActions.Lander.Movement.ReadValue<Vector2>();
   }
}
