using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour {
    public event Action<Vector2> OnMoveEvent;
    public event Action OnFireEvent;
    public event Action OnPauseEvent;

    public void OnMove(InputAction.CallbackContext context) {
        if (context.performed || context.canceled) {
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
            //Debug.Log("Input" + context.ReadValue<Vector2>());
        }
    }

    public void OnFire(InputAction.CallbackContext context) {
        if (context.performed) {
            OnFireEvent?.Invoke();
            Debug.Log("Fire input");
        }
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (context.performed) {
            OnPauseEvent?.Invoke();
        }
    }
}//