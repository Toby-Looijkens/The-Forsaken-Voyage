using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("References")]
    private PlayerController playerController;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lineRenderer;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;

    private Vector3 grapplePoint;

    [Header("Cooldown")] 
    public float grappleCooldown;
    private float grappleCooldownTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool isGrappling;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();

        if (Input.GetKeyUp(grappleKey)) StopGrapple();

        if (grappleCooldownTimer > 0) 
        { 
            grappleCooldownTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (isGrappling) 
        { 
            lineRenderer.SetPosition(0, gunTip.position);
        }
    }

    private void StartGrapple()
    {
        if (grappleCooldownTimer  > 0) 
        {
            return;
        }

        isGrappling = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        playerController.MovePlayerToGrapplePoint(grapplePoint);
    }

    private void StopGrapple()
    {
        isGrappling = false;
        grappleCooldownTimer = grappleCooldown;
        lineRenderer.enabled = false;
    }
}
