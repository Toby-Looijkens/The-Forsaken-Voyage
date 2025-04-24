using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CrawlerBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] CrawlerOrientation orientation;
    [SerializeField] CrawlerNavigation navigation;
    [SerializeField] GameObject crawlerWaypointMaker;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float speed;
    [SerializeField] float jumpCooldown = 10f;

    private float timer = 0;
    private Vector3 target = Vector3.zero;
    private float distance = 0;
    private Vector3 targetNormal = Vector3.zero;
    private bool canJump = false;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player != null && target == Vector3.zero && canJump)
        {
            Attack();
        }

        if (target != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            orientation.transform.rotation = Quaternion.FromToRotation(orientation.GetComponent<Transform>().up, targetNormal) * orientation.transform.rotation;
            if ((transform.position - target).magnitude < 0.05)
            {
                GameObject temp = Instantiate(crawlerWaypointMaker, target, Quaternion.FromToRotation(Vector3.up, targetNormal));
                target = Vector3.zero;
                navigation.crawlerWaypointMaker = temp;
                navigation.enabled = true;
                orientation.enabled = true;
            }
        }

        if (timer <= 0)
        {
            canJump = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hitInfo, 100f, layerMask))
        {
            Destroy(navigation.crawlerWaypointMaker);
            navigation.enabled = false;
            orientation.enabled = false;
            target = hitInfo.point;
            targetNormal = hitInfo.normal;
            canJump = false;
            timer = jumpCooldown;
        }           
    }
}
