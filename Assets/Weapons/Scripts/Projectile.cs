using Unity.AppUI.UI;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;

    private float projectileLifeTime = 2;

    private void Update()
    {
        if (projectileLifeTime <= 0)
        {
            Destroy(gameObject);
        }

        projectileLifeTime -= Time.deltaTime;

        transform.position += direction * 400 * Time.deltaTime;
    }
}
