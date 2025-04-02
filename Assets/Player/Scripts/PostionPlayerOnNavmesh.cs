using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PostionPlayerOnNavmesh : MonoBehaviour
{
    Node[] nodes;

    public Node targetNode;

    private void Start()
    {
    }

    private void Update()
    {
        nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
        targetNode = nodes[0];
        foreach (Node node in nodes) 
        {
            if (Vector3.Distance(transform.position, node.transform.position) < (Vector3.Distance(transform.position, targetNode.transform.position)))
            {
                targetNode = node;
            }
        }
    }
}
