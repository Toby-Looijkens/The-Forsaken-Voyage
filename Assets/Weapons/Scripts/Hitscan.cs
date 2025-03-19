using UnityEngine;
using System.Collections;

public class Hitscan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float range = 100f;
    public int damage = 10;
    public LayerMask hitLayers;
    public Transform muzzle;
    [SerializeField] PlayerInputManager playerinput;
    [SerializeField] Transform playercam;
    public Renderer enemyRenderer;
    public Material hitMaterial;
    private Material originalMaterial;
   
    void Start()
    {
        if (enemyRenderer == null)
            enemyRenderer = GetComponent<Renderer>();

        if (enemyRenderer != null)
            originalMaterial = enemyRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerinput.isTriggerPulled > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        RaycastHit hit;
        Vector3 rayDirection = playercam.forward;
        Debug.DrawRay(playercam.position, rayDirection * range, Color.red, 0.1f);
        if (Physics.Raycast(playercam.position, playercam.forward, out hit, range, hitLayers))
        {
            Debug.Log("test");
            HitAnimation();
            Debug.Log("Hit: " + hit.collider.name);
        }
    }

    public void HitAnimation()
    {
        if (enemyRenderer != null && hitMaterial != null)
        {
            StartCoroutine(ChangeMaterialCoroutine());
        }
    }

    private IEnumerator ChangeMaterialCoroutine()
    {
        enemyRenderer.material = hitMaterial; // Change to hit material
        yield return new WaitForSeconds(0.1f);  // Wait 1 second
        enemyRenderer.material = originalMaterial; // Revert back
    }
}
