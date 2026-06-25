using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour {
    public event Action<Vector2> OnMoveEvent;
    public event Action OnPauseEvent;

    public void OnMove(InputAction.CallbackContext context) {
        if (context.performed || context.canceled) {
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
            Debug.Log("Input" + context.ReadValue<Vector2>());
        }
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (context.performed) {
            OnPauseEvent?.Invoke();
        }
    }
}//