using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor.UIElements;
using Unity.Mathematics;
using UnityEditor;
using Unity.VisualScripting;

public class NodeGenerator : MonoBehaviour
{
    [Header("Nodes")]
    [SerializeField] private float distanceBetweenNodes = 1f;
    [SerializeField] LayerMask mask;

    public Node nodePrefab;
    public GameObject probePrefab;
    public List<Node> nodeList;

    public Crawler_Movement crawler;

    private bool canDrawGizmos;

    private float width;
    private float length;
    private float depth;

    private Vector3[,,] grid;

    private void Awake()
    {
        width = transform.localScale.x;
        length = transform.localScale.y;
        depth = transform.localScale.z;

        InitializeGrid();
        CreateNodes();
        CreateConnections();
        //NodeCleanup();
    }

    private void InitializeGrid()
    {
        int lenX = Mathf.FloorToInt(width / distanceBetweenNodes);
        int lenY = Mathf.FloorToInt(length / distanceBetweenNodes);
        int lenZ = Mathf.FloorToInt(depth / distanceBetweenNodes);

        grid = new Vector3[lenX, lenY, lenZ];

        //for (int x = 0; x < grid.GetLength(0); x++) 
        //{ 
        //    for (int y = 0; y < grid.GetLength(1); y++)
        //    {
        //        for(int z = 0; z < grid.GetLength(2); z++)
        //        {
        //            grid[x, y, z] = new Vector3(x * distanceBetweenNodes, y * distanceBetweenNodes, z * distanceBetweenNodes);
        //        }
        //    }
        //}
    }

    public void CreateNodes()
    {
        for (int x = 0; x < grid.GetLength(0); x++) 
        { 
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int z = 0; z < grid.GetLength(2); z++)
                {
                    Vector3 position = new Vector3(x * distanceBetweenNodes, y * distanceBetweenNodes, z * distanceBetweenNodes) + transform.position - transform.localScale / 2;
                    int maxColliders = 2;
                    Collider[] hitColliders = new Collider[maxColliders];
                    if (Physics.OverlapSphereNonAlloc(position, distanceBetweenNodes / 2, hitColliders, mask) == 0 && Physics.OverlapSphereNonAlloc(position, distanceBetweenNodes * 0.9f, hitColliders, mask) > 0)
                    {
                        Node node = Instantiate(nodePrefab, position, Quaternion.identity);
                        nodeList.Add(node);
                    } 
                }

            }
        }
    }

    public void CreateConnections()
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            for (int j = i + 1; j < nodeList.Count; j++)
            {
                if (Vector3.Distance(nodeList[i].transform.position, nodeList[j].transform.position) <= Mathf.Sqrt(3* (distanceBetweenNodes * distanceBetweenNodes)))
                {
                    ConnectNodes(nodeList[i], nodeList[j]);
                    ConnectNodes(nodeList[j], nodeList[i]);
                }
            }
        }
    }

    public void ConnectNodes(Node from, Node to)
    {
        if (from == to) { return; }

        from.connections.Add(to);
    }

    public void SpawnAI()
    {
        Node randomNode = nodeList[0];
    }

    private void OnDrawGizmos()
    {
        
    }

    private GameObject CreateProbe(Vector3 transform, float size)
    {
        GameObject probe = Instantiate(probePrefab, transform, Quaternion.identity);
        probe.GetComponent<SphereCollider>().radius = size;
        return probe;
    }
}
