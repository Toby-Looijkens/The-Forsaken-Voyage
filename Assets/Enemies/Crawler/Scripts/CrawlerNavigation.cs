using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CrawlerNavigation : MonoBehaviour
{
    [SerializeField] private Crawler_Waypoint_Maker crawlerWaypointMaker;
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject rotationComponent;

    [Header("Arcing raycast settings")]
    [SerializeField] int amountOfArcsPerRow = 10;
    [SerializeField] int rows = 1;
    [SerializeField] float spacingBetweenRows = 1f;
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
    }

    public void Move()
    {
        if (crawlerWaypointMaker.path.Count !=0)
        {
            transform.parent.position = Vector3.MoveTowards(transform.position, crawlerWaypointMaker.path[0], speed * Time.deltaTime);
            //if (RaycastExtensions.ArcCast(transform.position, transform.rotation, arcAngle, arcRadius, arcResolution, layer, out RaycastHit hit, turnOnDebugRays))
            //{
            //    transform.parent.position = Vector3.MoveTowards(transform.position, hit.point + hit.normal * 0.05f, speed * Time.deltaTime);
            //}
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
            transform.rotation = rotationComponent.transform.rotation;
            transform.parent.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
        }
    }
}
