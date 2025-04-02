using UnityEngine;

public class LootFloat : MonoBehaviour
{

    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float height = 0.5f;  

    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
