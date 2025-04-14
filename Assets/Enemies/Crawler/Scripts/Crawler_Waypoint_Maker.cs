using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Crawler_Waypoint_Maker : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] LayerMask mask;
    private NavMeshAgent agent;

    public List<Vector3> path = new List<Vector3>();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        agent.destination = player.transform.position;

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 50, mask))
        {
            if (path.Count == 0 || Vector3.Distance(path.Last(), hit.point) > 0.1f)
            {
                path.Add(hit.point);
            }
        }
    }
}
