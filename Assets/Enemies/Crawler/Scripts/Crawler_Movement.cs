using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crawler_Movement : MonoBehaviour
{
    [SerializeField] GameObject player;   
    
    public Node targetNode;
    public Node currentNode;
    public List<Node> path = new List<Node>();

    private void Update()
    {
        targetNode = player.GetComponent<PostionPlayerOnNavmesh>().targetNode;
        CreatePath();
    }

    public void CreatePath()
    {
        if (path.Count > 0)
        {
            int x = 0;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, path[x].transform.position.z), 5 * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, path[x].transform.position) < .1f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
        else
        {
            Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);

            if (currentNode == null)
            {
                currentNode = nodes[0];
            }

            while (path == null || path.Count == 0)
            {
                path = AStarManager.instance.GeneratePath(currentNode, targetNode);
            }
        }
    }
}
