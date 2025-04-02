using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Crawler_Movement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask mask;
    [SerializeField] float speed = 500f;

    public Node targetNode;
    public Node currentNode;
    public List<Node> path = new List<Node>();

    private void Update()
    {
        //targetNode = player.GetComponent<PostionPlayerOnNavmesh>().targetNode;
        //CreatePath();
        agent.destination = player.transform.position;
    }

   

    public void Move()
    {
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
