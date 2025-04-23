using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class Hitscan : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private Transform muzzle;
    [SerializeField] private PlayerInputManager playerinput;
    [SerializeField] private Transform playercam;
    private Renderer enemyRenderer;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material originalMaterial;
    private GameObject hitObject;
    private bool finishanimation = true;

    [Header("Weapon Stats")]
    [SerializeField] int fireRate = 400;
    [SerializeField] private int damage = 1;
    [SerializeField] bool semiAuto = true;
    [SerializeField] int magazine = 7;
    [SerializeField] int reserveAmmo = 35;
    [SerializeField] int reserveAmmoTotal = 63;

    private bool hasReleasedTrigger = true;
    private float timeSinceLastShot = 0;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerinput.isTriggerPulled > 0)
        {
            Fire();
        } else
        {
            hasReleasedTrigger = true;
        }
    }

    void Fire()
    {
        if (semiAuto && !hasReleasedTrigger)
        {
            return;
        }

        if (timeSinceLastShot > 0) 
        {
            timeSinceLastShot -= Time.deltaTime;
            return;
        }

        RaycastHit hit;
        Vector3 rayDirection = playercam.forward;
        Debug.DrawRay(playercam.position, rayDirection * range, Color.red, 0.1f);
        if (Physics.Raycast(playercam.position, playercam.forward, out hit, range, hitLayers))
        {
            hitObject = hit.collider.gameObject;
            //enemyRenderer = hitObject.GetComponent<Renderer>();
            //originalMaterial = enemyRenderer.material;
            //StartCoroutine(HitAnimation());
            //finishanimation = false;
            Debug.Log("Hit");
            hitObject.SendMessage("Damage");
        }
        hasReleasedTrigger = false;
        timeSinceLastShot = 60f / fireRate;
    }

    private IEnumerator HitAnimation()
    {
        enemyRenderer.material = hitMaterial; // Change to hit material
        yield return new WaitForSeconds(0.1f);  // Wait 1 second
        enemyRenderer.material = originalMaterial; // Revert back
        Health health = hitObject.GetComponent<Health>();
        health.Damage();
        finishanimation = true;
    }
}
