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
    [SerializeField] private GameObject projectile;
    private GameObject hitObject;
    private bool finishanimation = true;

    [Header("Weapon Stats")]
    [SerializeField] int fireRate = 400;
    [SerializeField] private int damage = 1;
    [SerializeField] bool semiAuto = true;
    [SerializeField] int magazine = 7;
    [SerializeField] int reserveAmmo = 35;
    [SerializeField] int reserveAmmoTotal = 63;
    [SerializeField] float recoilPower = 500;

    private PlayerController playerController;

    private bool hasReleasedTrigger = true;
    private float timeSinceLastShot = 0;

    public int ammo = 30;
    private bool isShooting = false;
    public Damage dmgScript;
    public Energy engScript;
    
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerinput.isTriggerPulled > 0 && isShooting == false && ammo > 0)
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

        GameObject temp = Instantiate(projectile, playercam.position + playercam.right * 0.5f + playercam.up * -0.5f, Quaternion.identity);
        temp.GetComponent<Projectile>().direction = playercam.forward;

        playerController.Recoil(recoilPower);

        RaycastHit hit;
        Vector3 rayDirection = playercam.forward;
        Debug.DrawRay(playercam.position, rayDirection * range, Color.red, 0.1f);
        if (Physics.Raycast(playercam.position, playercam.forward, out hit, range, hitLayers))
        {
            hitObject = hit.collider.gameObject;
            hitObject.SendMessage("Damage");
        }
        hasReleasedTrigger = false;
        timeSinceLastShot = 60f / fireRate;
    }
}
