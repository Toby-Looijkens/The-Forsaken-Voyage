using System.Threading;
using UnityEngine;

public class Rotate_Body : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private LayerMask terrain;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float time;
    private Vector3 normal;

    private void OnCollisionEnter(Collision collision)
    {
        normal = collision.contacts[0].normal;
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit info = new RaycastHit();
        if (Physics.Raycast(ray, out info, 100, terrain)) {
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, info.normal);
        }
        Debug.Log(info.normal);
    }
}
