using UnityEngine;
using UnityEngine.SceneManagement;

public class LootManager : MonoBehaviour
{
    
    [SerializeField] public int totalValue;
    [SerializeField] public int totalAmount;
    public int quota;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float weight;
    [SerializeField] private float weightFill;
    public UIBars uiBars;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        quota = Random.Range(10000,20000);
        uiManager.SetQuotaUI(quota);
    }

    // Update is called once per frame
    void Update()
    {
        playerController.acceleration = 20f - (4f * totalAmount);

        if (playerController.acceleration < 1f)
        {
            playerController.acceleration = 1f;
        }

        weight = totalAmount * 4;

        if (weight > 20)
        {
            weight = 20;
        }

        weightFill = weight / 20;
        uiBars.WeightBar(weightFill);

        
    }

    public void Collect(int lootValue)
    {
        totalValue = totalValue + lootValue;
        totalAmount = totalAmount + 1;
        uiManager.UpdateHolding(totalValue);
    }
}
