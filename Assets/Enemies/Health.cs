using UnityEngine;

public class Health : MonoBehaviour
{

    public float health = 10;
    [SerializeField] GameObject parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            Debug.Log(health);
            Destroy(parent);
        }
    }

    public void Damage(float damage)
    {
        health = health - damage;
    }
}
