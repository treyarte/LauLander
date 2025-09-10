using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] CargoArea cargoArea;
    private const string IS_DOOR_OPEN = "IsDoorOpen";
    private static readonly int IsDoorOpen = Animator.StringToHash(IS_DOOR_OPEN);
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cargoArea.OnCargoDropOff += CargoArea_OnDropOff;
    }

    private void CargoArea_OnDropOff(object sender, EventArgs e)
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        animator.SetBool(IsDoorOpen, true);
    }
    
    private void CloseDoor()
    {
        animator.SetBool(IsDoorOpen, false);
    }
}
