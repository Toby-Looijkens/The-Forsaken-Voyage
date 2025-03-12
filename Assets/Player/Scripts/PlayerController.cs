using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [Header("Player movement")]
    [SerializeField] float acceleration = 5f;
    [SerializeField] float deceleration = 5f;
    [SerializeField] float grapplingHookStrength = 2f;

    [Header("Camera movement")]
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float horizontalSensitivity = 0.8f;
    [SerializeField] float verticalSensitivity = 0.5f;

    [Header("Extra")]
    [SerializeField] int recoilpower = 5;
    [SerializeField] Rigidbody rigidbody;

    private bool isPlayerLocked = false;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        rigidbody.angularVelocity = Vector3.zero;
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
        if (rigidbody.linearVelocity.magnitude > 3)
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
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(-temp.y * horizontalSensitivity, Mathf.Clamp(temp.x * verticalSensitivity, -25, 85), 0));
        }
        else 
        {
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(-temp.y * horizontalSensitivity, temp.x * verticalSensitivity, 0));
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

        rigidbody.linearVelocity += relativeMovement * acceleration * Time.deltaTime;
        //transform.position += globalSpeedVector * Time.deltaTime;
    }

    private void RotatePlayer()
    { 
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(0, 0, playerInputManager.rollVector.y * rotationSpeed * Time.deltaTime));
    }

    private void SlowDownPlayer()
    {
        Vector3 speedVector = rigidbody.linearVelocity;
        Vector3 invertedSpeedVector = speedVector * -1 * deceleration * Time.deltaTime;

        if (Mathf.Abs(speedVector.x) >= 0 && Mathf.Abs(speedVector.x) <= Mathf.Abs(invertedSpeedVector.x))
        {
            speedVector.x = 0;
        }
        else
        {
            speedVector.x += invertedSpeedVector.x;
        }

        if (Mathf.Abs(speedVector.y) >= 0 && Mathf.Abs(speedVector.y) <= Mathf.Abs(invertedSpeedVector.y))
        {
            speedVector.y = 0;
        }
        else
        {
            speedVector.y += invertedSpeedVector.y;
        }

        if (Mathf.Abs(speedVector.z) >= 0 && Mathf.Abs(speedVector.z) <= Mathf.Abs(invertedSpeedVector.z))
        {
            speedVector.z = 0;
        }
        else
        {
            speedVector.z += invertedSpeedVector.z;
        }

        rigidbody.linearVelocity = speedVector;
    }

    private void Shoot()
    {
        if (playerInputManager.isTriggerPulled > 0)
        {
            Recoil(recoilpower);
        }
    }

    public void Recoil(int power)
    {
        Vector3 input = playerInputManager.movementVector;

        //Get camera normals
        Vector3 forward = Camera.main.transform.forward;

        //Movement based on where player is looking
        Vector3 forwardRelative = forward * power / 100;


        rigidbody.linearVelocity -= forwardRelative;
    }

    public void MovePlayerToGrapplePoint(Vector3 endPoint)
    {
        Vector3 grapple = endPoint - rigidbody.position;
        rigidbody.linearVelocity += grapple * grapplingHookStrength;
    }
}
