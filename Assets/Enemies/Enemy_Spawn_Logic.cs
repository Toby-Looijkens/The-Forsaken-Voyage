using UnityEngine;

public class Enemy_Spawn_Logic : MonoBehaviour
{
    [SerializeField] GameObject crawler;
    [SerializeField] GameObject crawlerWaypointMaker;

    [SerializeField] float spawnCooldown = 20;
    private float maxTime;

    private void Start()
    {
        maxTime = spawnCooldown;
    }

    void Update()
    {
        if (spawnCooldown <= 0)
        {
            spawnCooldown = maxTime;
            SpawnEnemy();
        } else
        {
            spawnCooldown -= Time.deltaTime;
        }
    }

    public void SpawnEnemy()
    {
        GameObject _crawlerWaypointMaker = Instantiate(crawlerWaypointMaker, transform.position, Quaternion.identity);
        GameObject _crawler = Instantiate(crawler, transform.position, Quaternion.identity);
        _crawler.GetComponentInChildren<CrawlerNavigation>().crawlerWaypointMaker = _crawlerWaypointMaker;
    }
}
