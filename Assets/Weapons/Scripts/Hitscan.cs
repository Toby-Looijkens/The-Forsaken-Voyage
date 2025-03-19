using UnityEngine;
using System.Collections;

public class Hitscan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float range = 100f;
    [SerializeField] private int damage = 10;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private Transform muzzle;
    [SerializeField] private PlayerInputManager playerinput;
    [SerializeField] private Transform playercam;
    private Renderer enemyRenderer;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material originalMaterial;
    private GameObject hitObject;
    private bool finishanimation = true;
   
    void Start()
    {

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
        if (finishanimation == true) 
        {
            RaycastHit hit;
            Vector3 rayDirection = playercam.forward;
            Debug.DrawRay(playercam.position, rayDirection * range, Color.red, 0.1f);
            if (Physics.Raycast(playercam.position, playercam.forward, out hit, range, hitLayers))
            {
                hitObject = hit.collider.gameObject;
                enemyRenderer = hitObject.GetComponent<Renderer>();
                originalMaterial = enemyRenderer.material;
                StartCoroutine(HitAnimation());
                finishanimation = false;
                Debug.Log(originalMaterial);
            }
        }
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
