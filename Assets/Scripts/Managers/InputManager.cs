using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerActions inputActions;

    InputAction movement;
    InputAction jump;
    InputAction phase;
    InputAction dash;
    InputAction pause;

    private void Awake()
    {
        inputActions = new PlayerActions();

        movement = inputActions.Movement.Movement;
        jump = inputActions.Movement.Jump;
        phase = inputActions.Movement.Phasing;
        dash = inputActions.Movement.Dash;
        pause = inputActions.Movement.Pause;
    }

    private void OnEnable()
    {
        inputActions.Enable();

        movement.performed += OnMove;
        movement.canceled += OnMove;

        jump.performed += OnJump;

        phase.performed += OnPhase;

        dash.performed += OnDash;

        pause.performed += OnPause;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        movement.performed -= OnMove;
        movement.canceled -= OnMove;

        jump.performed -= OnJump;

        phase.performed -= OnPhase;

        dash.performed -= OnDash;

        pause.performed -= OnPause;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        PlayerManager.Instance.OnMove(input);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        PlayerManager.Instance.OnJump();
    }

    private void OnPhase(InputAction.CallbackContext context)
    {
        GameManager.Instance.OnPhase();
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        PlayerManager.Instance.OnDash();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        GameManager.Instance.PauseMenuActive();
    }
}
