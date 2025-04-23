using UnityEngine;

public class Damage : MonoBehaviour
{

    public float hp = 100;
    public UIBars uiBars;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        hp = hp - damage;
        uiBars.HealthBar(hp / 100);
    }
}
