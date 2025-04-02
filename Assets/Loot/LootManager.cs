using UnityEngine;

public class LootManager : MonoBehaviour
{
    
    [SerializeField] public int totalValue;
    [SerializeField] public int totalAmount;
    [SerializeField] private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerController.acceleration);
        playerController.acceleration = 20f - (4f * totalAmount);

        if (playerController.acceleration < 1f)
        {
            playerController.acceleration = 1f;
        }
    }

    public void Collect(int lootValue)
    {
        totalValue = totalValue + lootValue;
        totalAmount = totalAmount + 1;
    }
}
