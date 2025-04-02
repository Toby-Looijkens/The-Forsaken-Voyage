using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Crawler_Movement : MonoBehaviour
{
    //[SerializeField] GameObject player;
    //[SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask mask;
    [SerializeField] float speed = 500f;

    public Node targetNode;
    public Node currentNode;
    public List<Node> path = new List<Node>();

    private void Update()
    {
        //targetNode = player.GetComponent<PostionPlayerOnNavmesh>().targetNode;
        //CreatePath();
        //agent.destination = player.transform.position;
        Move();
    }

    static public bool ArcCast(Vector3 center, Quaternion rotation, float angle, float radius, int resolution, LayerMask layer, out RaycastHit hit, Color color)
    {
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
              return true;
            }
        }

        hit = new RaycastHit();
        return false;
    }

    public void Move()
    {
        float arcAngle = 270;
        float arcRadius = 0.5f;
        int arcResolution = 6;

        if (ArcCast(transform.position, transform.rotation, arcAngle, arcRadius, arcResolution, mask, out RaycastHit hit, Color.red))
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        //for (int i = 1; i < 8; i++)
        //{
        //    if (ArcCast(transform.position, Quaternion.Euler(transform.up) * Quaternion.Euler(0, 45 * i, 0), arcAngle, arcRadius, arcResolution, mask, out RaycastHit _hit, Color.blue))
        //    {
        //        transform.rotation = Quaternion.FromToRotation(transform.up, _hit.normal) * transform.rotation;
        //    }
        //}
    }

    //    public void CreatePath()
    //    {
    //        if (path.Count > 0)
    //        {
    //            int x = 0;
    //            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, path[x].transform.position.z), 5 * Time.deltaTime);

    //            if (Vector3.Distance(transform.position, path[x].transform.position) < .1f)
    //            {
    //                currentNode = path[x];
    //                path.RemoveAt(x);
    //            }
    //        }
    //        else
    //        {
    //            Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);

    //            if (currentNode == null)
    //            {
    //                currentNode = nodes[0];
    //            }

    //            while (path == null || path.Count == 0)
    //            {
    //                path = AStarManager.instance.GeneratePath(currentNode, targetNode);
    //            }
    //        }
    //    }
}
