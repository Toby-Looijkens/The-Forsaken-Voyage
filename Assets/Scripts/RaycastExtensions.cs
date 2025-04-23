using UnityEngine;

public class RaycastExtensions : MonoBehaviour
{
    static public bool ArcCast(Vector3 center, Quaternion rotation, float angle, float radius, int resolution, LayerMask layer, out RaycastHit hit, bool turnOnDebugRays)
    {
        rotation *= Quaternion.Euler(-angle / 2, 0, 0);

        for (int i = 0; i < resolution; i++)
        {
            Vector3 A = center + rotation * Vector3.forward * radius;
            rotation *= Quaternion.Euler(angle / resolution, 0, 0);
            Vector3 B = center + rotation * Vector3.forward * radius;
            Vector3 AB = B - A;

            if (turnOnDebugRays)
            {
                Debug.DrawRay(A, AB, Color.red);
            }

            if (Physics.Raycast(A, AB, out hit, AB.magnitude * 1.001f, layer))
            {
                if (turnOnDebugRays)
                {
                    Debug.DrawRay(hit.point, hit.normal * 0.05f, Color.white);
                }
                return true;
            }
        }
        hit = new RaycastHit();
        return false;
    }
}
