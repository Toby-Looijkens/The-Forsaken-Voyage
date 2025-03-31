using UnityEngine;

public class LootManager : MonoBehaviour
{
    
    [SerializeField] public int totalValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect(int lootValue)
    {
        totalValue = totalValue + lootValue;
    }
}
