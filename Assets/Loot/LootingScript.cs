using Mono.Cecil.Cil;
using UnityEngine;
using System.Collections;
using TMPro.EditorUtilities;
using Unity.VisualScripting.Antlr3.Runtime;

public class LootingScript : MonoBehaviour
{
    
    [SerializeField] private int lootActive;
    [SerializeField] private int lootValue;
    [SerializeField] public GameObject player;
    [SerializeField] private float distance;
    public PlayerInputManager playerinput;
    [SerializeField] private LootManager lootManager;
    [SerializeField] private UIManager uiManager;
    private bool tipActive = false;
       
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        lootActive = Random.Range(0,2);
        lootValue = Random.Range(100,10000);

        if (lootActive == 0) 
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var lootPosition = gameObject.transform.position;
        var playerPosition = player.transform.position;
        distance = Vector3.Distance(lootPosition,playerPosition);

        if (distance < 3) 
        {
            uiManager.TipLootCollectOn();
            tipActive = true;
        }
        else if (tipActive == true)
        {
            uiManager.TipLootCollectOff();
            tipActive = false;
        }

        if (playerinput.isInteracting > 0)
        {
            Loot();
        }
    }

    void Loot()
    {
        if (lootActive == 1)
        {
            if (distance < 3) 
            {
                gameObject.SetActive(false);
                lootManager.Collect(lootValue);
                uiManager.TipLootCollectOff();
            }
        }
    }
}
