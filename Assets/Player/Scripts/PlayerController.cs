using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 globalSpeedVector = new Vector3(0, 0, 0);

    private PlayerInputManager playerInputManager;

    [SerializeField] float acceleration = 5f;
    [SerializeField] float deceleration = 5f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float horizontalSensitivity = 0.8f;
    [SerializeField] float verticalSensitivity = 0.5f;
    [SerializeField] int recoilpower = 5;

    private bool isPlayerLocked = false;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (playerInputManager.movementVector == Vector3.zero)
        {
            SlowDownPlayer();
        }

        if (!isPlayerLocked) 
        {
            MovePlayer();
            RotatePlayer();
        }
        Shoot();
    }

    private void OnStop()
    {
        Debug.Log(globalSpeedVector.magnitude);
        if (globalSpeedVector.magnitude > 3)
        {
            return;
        }

        if (isPlayerLocked)
        {
            isPlayerLocked = false;
        } else
        {
            isPlayerLocked = true;
        }
    }

    private void OnLook(InputValue value)
    {
        Vector2 temp = value.Get<Vector2>();

        if (isPlayerLocked)
        {
            transform.Rotate(-temp.y * horizontalSensitivity, Mathf.Clamp(temp.x * verticalSensitivity, -25, 85), 0);
        }
        else 
        {
            transform.Rotate(-temp.y * horizontalSensitivity, temp.x * verticalSensitivity, 0);
        }
    }

    private void MovePlayer()
    {
        Vector3 input = playerInputManager.movementVector;

        //Get camera normals
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        Vector3 up = Camera.main.transform.up;

        //Movement based on where player is looking
        Vector3 forwardRelative = forward * input.z;
        Vector3 rightRelative = right * input.x;
        Vector3 upRelative = up * input.y;

        Vector3 relativeMovement = forwardRelative + rightRelative + upRelative;

        globalSpeedVector += relativeMovement * acceleration * Time.deltaTime;
        transform.position += globalSpeedVector * Time.deltaTime;
    }

    private void RotatePlayer()
    { 
        transform.Rotate(0, 0, playerInputManager.rollVector.y * rotationSpeed * Time.deltaTime);
    }

    private void SlowDownPlayer()
    {
        Vector3 speedVector = globalSpeedVector;
        Vector3 invertedSpeedVector = speedVector * -1 * deceleration * Time.deltaTime;

        if (Mathf.Abs(speedVector.x) >= 0 && Mathf.Abs(speedVector.x) <= Mathf.Abs(invertedSpeedVector.x))
        {
            globalSpeedVector.x = 0;
        } else
        {
            globalSpeedVector.x += invertedSpeedVector.x;
        }

        if (Mathf.Abs(speedVector.y) >= 0 && Mathf.Abs(speedVector.y) <= Mathf.Abs(invertedSpeedVector.y))
        {
            globalSpeedVector.y = 0;
        }
        else
        {
            globalSpeedVector.y += invertedSpeedVector.y;
        }

        if (Mathf.Abs(speedVector.z) >= 0 && Mathf.Abs(speedVector.z) <= Mathf.Abs(invertedSpeedVector.z))
        {
            globalSpeedVector.z = 0;
        }
        else
        {
            globalSpeedVector.z += invertedSpeedVector.z;
        }
    }

    private void Shoot()
    {
        if (playerInputManager.isTriggerPulled > 0)
        {
            recoil(recoilpower);
        }
    }

    public void recoil(int power)
    {
        Vector3 input = playerInputManager.movementVector;

        //Get camera normals
        Vector3 forward = Camera.main.transform.forward;

        //Movement based on where player is looking
        Vector3 forwardRelative = forward * power;


        globalSpeedVector -= forwardRelative;
    }
}
