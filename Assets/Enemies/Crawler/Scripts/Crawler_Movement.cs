using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System;

public class Crawler_Movement : MonoBehaviour
{
    [SerializeField] private Crawler_Waypoint_Maker crawlerWaypointMaker;
    [SerializeField] float speed = 5f;

    [Header("Arcing raycast settings")]
    [SerializeField] int amountOfArcsPerRow = 10;
    [SerializeField] int rows = 1;
    [SerializeField] float spacingBetweenRows = 1.5f;
    [SerializeField] float arcAngle = 270;
    [SerializeField] float arcRadius = 1f;
    [SerializeField] int arcResolution = 6;
    [SerializeField] bool turnOnDebugRays = false;
    [SerializeField] LayerMask layer;

    private void Update()
    {
        if (crawlerWaypointMaker == null)
        {
            MoveForward();
        }
        else
        {
            Move();
        }
        AlignToSurface();
    }

    public void Move()
    {
        if (crawlerWaypointMaker.path.Count !=0)
        {
            transform.position = Vector3.MoveTowards(transform.position, crawlerWaypointMaker.path[0], speed * Time.deltaTime);
        }

        if (crawlerWaypointMaker.path.Count !=0 && (crawlerWaypointMaker.path[0] - transform.position).magnitude < 0.1f)
        {
            crawlerWaypointMaker.path.RemoveAt(0);
        }
    }

    public void MoveForward()
    {
        if (RaycastExtensions.ArcCast(transform.position, transform.rotation, arcAngle, arcRadius, arcResolution, layer, out RaycastHit hit, turnOnDebugRays))
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
        }
    }

    private void AlignToSurface()
    {
        Vector3 averageNormal = Vector3.zero;


        // Throw out arcing raycast to find surface normals
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 0; j < amountOfArcsPerRow; j++)
            {
                if (RaycastExtensions.ArcCast(transform.position, transform.rotation * Quaternion.Euler(0, 360 / amountOfArcsPerRow * j, 0), arcAngle, arcRadius * (spacingBetweenRows * i), arcResolution, layer, out RaycastHit _hit, turnOnDebugRays))
                {
                    // Calculate a normal to better align object to terrain
                    Debug.DrawRay(transform.position, -Vector3.Cross(Vector3.Cross(transform.up, _hit.point - transform.position), _hit.point - transform.position), Color.green);
                    averageNormal += Vector3.Cross(Vector3.Cross(transform.up, _hit.point - transform.position), _hit.point - transform.position );
                }
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, -averageNormal) * transform.rotation, 0.1f);
        Debug.DrawRay(transform.position, -averageNormal * 5, Color.magenta);
    }
}
