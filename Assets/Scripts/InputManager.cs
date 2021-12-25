using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    private PlayerInputActions _playerInputActions;
    private InputAction _movement, _jump, _mouse;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _movement = _playerInputActions.Player.Movement;
        _movement.Enable();
        
        _jump = _playerInputActions.Player.Jump;
        _jump.Enable();

        _mouse = _playerInputActions.Player.MouseMove;
        _mouse.Enable();
    }

    private void OnDisable()
    {
        _movement.Disable();
        _jump.Disable();
    }

    public Vector2 GetMovementInput()
    {
        return _movement.ReadValue<Vector2>();
    }
    
    public bool GetJumpInputThisFrame()
    {
        return _jump.triggered; 
    }

    public Vector2 GetMouseDeltaInput()
    {
        return _mouse.ReadValue<Vector2>();
    }
}
