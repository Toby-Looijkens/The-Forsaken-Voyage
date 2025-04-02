using System.Threading;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Rotate_Body : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    private int amountOfArcs = 10;
    [SerializeField] float speed = 1f;
    private Vector2 movementVector = Vector2.zero;

    private void Update()
    {
            Move();
    }

    static public bool ArcCast(Vector3 center, Quaternion rotation, float angle, float radius, int resolution, LayerMask layer, out RaycastHit hit, out float hitStrength, Color color)
    {
        hitStrength = resolution;
        rotation *= Quaternion.Euler(-angle / 2, 0, 0);

        for (int i = 0; i < resolution; i++)
        {
            Vector3 A = center + rotation * Vector3.forward * radius;
            rotation *= Quaternion.Euler(angle / resolution, 0, 0);
            Vector3 B = center + rotation * Vector3.forward * radius;
            Vector3 AB = B - A;

            Debug.DrawRay(A, AB, color);
            if (Physics.Raycast(A, AB, out hit, AB.magnitude * 1.001f, layer))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.white);
                hitStrength -= 1 - (hit.point - A).magnitude;
                return true;
            }

            hitStrength -= AB.magnitude;
        }
        hit = new RaycastHit();
        return false;
    }

    public void Move()
    {
        float arcAngle = 270;
        float arcRadius = 1.2f;
        int arcResolution = 6;

        int amountOfHits = 0;
        Debug.DrawRay(transform.position, transform.up, Color.blue);

        Vector3 averageNormal = Vector3.zero;

        if (ArcCast(transform.position, transform.rotation, arcAngle, arcRadius, arcResolution, layer, out RaycastHit hit, out float hitStrength, Color.green))
        {
            averageNormal += hit.normal.normalized;
            transform.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
        }

        for (int i = 1; i < amountOfArcs; i++)
        {
            if (ArcCast(transform.position, transform.rotation * Quaternion.Euler(0, 360 / amountOfArcs * i, 0), arcAngle, arcRadius, arcResolution, layer, out RaycastHit _hit, out float _hitStrength, Color.yellow))
            {
                averageNormal += _hit.normal.normalized;
            }
        }

        for (int i = 0; i < amountOfArcs; i++)
        {
            if (ArcCast(transform.position, transform.rotation * Quaternion.Euler(0, 360 / amountOfArcs * i, 0), arcAngle, arcRadius * 1.4f, arcResolution, layer, out RaycastHit _hit_, out float _hitStrength, Color.yellow))
            {
                averageNormal += _hit_.normal.normalized;
            }
        }

        transform.rotation = Quaternion.FromToRotation(transform.up, averageNormal.normalized) * transform.rotation;
        Debug.DrawRay(transform.position, averageNormal, Color.magenta);
        Debug.Log(averageNormal);

    }
}

