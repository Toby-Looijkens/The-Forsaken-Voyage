using UnityEngine;

public class Health : MonoBehaviour
{

    public int health = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void Damage()
    {
        health = health - 1;
    }
}
