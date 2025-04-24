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
    [SerializeField] private float damage = 2;
    [SerializeField] bool semiAuto = true;
    [SerializeField] public int ammo = 20;
    [SerializeField] int magazineSize = 20;
    [SerializeField] public int reserveAmmo = 60;
    [SerializeField] int reserveAmmoTotal = 120;
    [SerializeField] float recoilPower = 30;
    [SerializeField] float reloadTime = 2.25f;

    private PlayerController playerController;

    private bool hasReleasedTrigger = true;
    private float timeSinceLastShot = 0;

    private bool isShooting = false;
    public bool isReloading = false;
    private float reloadDuration = 0;
    public Damage dmgScript;
    public Energy engScript;
    
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) 
        { 
            if (reloadDuration >= reloadTime)
            {
                Reload();
                reloadDuration = 0;
                isReloading = false;
            } 
            else
            {
                reloadDuration += Time.deltaTime;
            }
        }

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
        if (ammo <= 0 || isReloading == true) return;

        if (semiAuto && !hasReleasedTrigger) return;

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
            hitObject.SendMessage("Damage", damage);
        }
        hasReleasedTrigger = false;
        timeSinceLastShot = 60f / fireRate;
        ammo--;
    }

    private void OnReload()
    {
      isReloading = true;
    }

    private void Reload()
    {
        if (reserveAmmo <= 0) return;

        reserveAmmo += ammo;

        if (reserveAmmo >= magazineSize)
        {
            ammo = magazineSize;
            reserveAmmo -= magazineSize;
        }
        else
        {
            ammo = reserveAmmo;
            reserveAmmo = 0;
        }
    }
}
