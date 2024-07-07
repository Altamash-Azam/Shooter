using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    playerMotor motor;
    PlayerLook look;

    private void Awake() {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<playerMotor>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Crouch.performed += ctx => motor.Crouch();
    }

    private void FixedUpdate() {
        // tell the player motor to move using the value from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate() {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable() {
        onFoot.Enable();
    }

    private void OnDisable() {
        onFoot.Disable();
    }
}
