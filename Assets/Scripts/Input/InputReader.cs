using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputReader : MonoBehaviour {
    public event Action<Vector2> OnMoveEvent;
    public event Action OnPauseEvent;

    public void OnMove(InputAction.CallbackContext context) {
        if (context.performed) {
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
            Debug.Log("Input" + context);
        }
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (context.performed) {
            OnPauseEvent?.Invoke();
        }
    }
}