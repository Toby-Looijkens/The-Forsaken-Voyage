using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    public Vector3 movementVector {  get; private set; }
    public Vector2 rollVector { get; private set; }
    public float isTriggerPulled { get; private set; }

    private void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector3>();
    }

    private void OnRoll(InputValue value)
    {
        rollVector = value.Get<Vector2>();
    }

    private void OnShoot(InputValue value)
    {
        isTriggerPulled = value.Get<float>();
    }
}
