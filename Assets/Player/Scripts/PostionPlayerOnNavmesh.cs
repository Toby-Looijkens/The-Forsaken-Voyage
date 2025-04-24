using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PostionPlayerOnNavmesh : MonoBehaviour
{
    public Vector3 target;

    List<Vector3> hits;
    [SerializeField] LayerMask layer;

    private void Start()
    {
    }

    private void Update()
    {
        hits = new List<Vector3>();
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit up, 25, layer))
        {
            hits.Add(up.point);
        }

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit down, 25, layer))
        {
            hits.Add(down.point);
        }

        for (int i = 0; i < 8; i++) 
        {
            if (Physics.Raycast(transform.position, Quaternion.AngleAxis(45 *i, Vector3.up) * Vector3.forward, out RaycastHit _hit, 25, layer))
            {
                hits.Add(_hit.point);
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (Physics.Raycast(transform.position, Quaternion.AngleAxis(45 * i, Vector3.up) * (Vector3.forward + Vector3.up), out RaycastHit __hit, 25, layer))
            {
                hits.Add(__hit.point);
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (Physics.Raycast(transform.position, Quaternion.AngleAxis(45 * i, Vector3.up) * (Vector3.forward + Vector3.down), out RaycastHit ___hit, 25, layer))
            {
                hits.Add(___hit.point);
            }
        }

        foreach (Vector3 hit in hits)
        {
            if ((transform.position - hit).magnitude < (transform.position - target).magnitude) target = hit;
        }
    }
}
