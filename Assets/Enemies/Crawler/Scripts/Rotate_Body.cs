using System.Data;
using System.Threading;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Rotate_Body : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private Crawler_Movement path;
    private int amountOfArcs = 10;
    private int rows = 5;
    [SerializeField] float speed = 1f;
    private Vector2 movementVector = Vector2.zero;

    private void Update()
    {
            Move();
    }

    static public bool ArcCast(Vector3 center, Quaternion rotation, float angle, float radius, int resolution, LayerMask layer, out RaycastHit hit)
    {
        rotation *= Quaternion.Euler(-angle / 2, 0, 0);

        for (int i = 0; i < resolution; i++)
        {
            Vector3 A = center + rotation * Vector3.forward * radius;
            rotation *= Quaternion.Euler(angle / resolution, 0, 0);
            Vector3 B = center + rotation * Vector3.forward * radius;
            Vector3 AB = B - A;

            Debug.DrawRay(A, AB, Color.red);
            if (Physics.Raycast(A, AB, out hit, AB.magnitude * 1.001f, layer))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.white);
                return true;
            }
        }
        hit = new RaycastHit();
        return false;
    }

    public void Move()
    {
        float arcAngle = 270;
        float arcRadius = 1f;
        int arcResolution = 6;

        int amountOfHits = 0;
        Debug.DrawRay(transform.position, transform.up, Color.blue);
       
        Vector3 averageNormal = Vector3.zero;

        if (ArcCast(transform.position, transform.rotation, arcAngle, arcRadius, arcResolution, layer, out RaycastHit hit))
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
        }

        // Throw out arcing raycast to find surface normals
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 0; j < amountOfArcs; j++)
            {
                if (ArcCast(transform.position, transform.rotation * Quaternion.Euler(0, 360 / amountOfArcs * j, 0), arcAngle, arcRadius * (1 + 0.2f * i), arcResolution, layer, out RaycastHit _hit))
                {
                    averageNormal += _hit.normal.normalized;
                }
            }
        }


        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, averageNormal.normalized) * transform.rotation, 0.1f);
        Debug.DrawRay(transform.position, averageNormal, Color.magenta);
    }
}

