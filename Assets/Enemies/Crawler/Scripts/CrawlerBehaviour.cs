using Unity.VisualScripting;
using UnityEngine;

public class CrawlerBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody player;
    [SerializeField] CrawlerOrientation orientation;
    [SerializeField] CrawlerNavigation navigation;
    [SerializeField] GameObject crawlerWaypointMaker;
    [SerializeField] float speed;
    [SerializeField] float jumpCooldown = 10f;

    private float timer = 0;
    private Vector3 target = Vector3.zero;
    private float distance = 0;
    private Vector3 targetNormal = Vector3.zero;
    private bool canJump = false;

    void Update()
    {
        if (player != null && target == Vector3.zero && canJump == true && (player.position + player.linearVelocity - transform.position).magnitude < 5)
        {
            Attack();
        }

        if (target != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(GetComponentInParent<Transform>(orientation).rotation, Quaternion.Euler(targetNormal), distance - (target - transform.position).magnitude);
            if ((transform.position - target).magnitude < 0.05)
            {
                target = Vector3.zero;

                GameObject temp = Instantiate(crawlerWaypointMaker, transform.position, Quaternion.identity);
                navigation.crawlerWaypointMaker = temp;
                navigation.enabled = true;
                orientation.enabled = true;
            }
        }

        if (timer <= 0) 
        { 
            canJump = true;
        } else
        {
            timer -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        if (Physics.Raycast(transform.position, player.position + player.linearVelocity - transform.position, out RaycastHit hitInfo))
        {
            Debug.Log("test");
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
