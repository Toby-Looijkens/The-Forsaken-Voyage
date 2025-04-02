using System.Drawing;
using UnityEngine;

public class ProbeScript : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    public bool isTouchingTerrain;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == mask)
        {
            isTouchingTerrain = true;
        }
    }
}
