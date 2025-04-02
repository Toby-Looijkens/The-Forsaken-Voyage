using UnityEngine;
using System.Collections;
using TMPro;

public class DropOff : MonoBehaviour
{
    [SerializeField] private LootManager lootManager;
    [SerializeField] public int total;
    [SerializeField] public GameObject player;
    [SerializeField] public PlayerInputManager playerinput;
    [SerializeField] private float distance;
    private bool finishCollect = true;
    public TextMeshProUGUI uiTotal;
    private bool tipActive = false;

    [SerializeField] private UIManager uiManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (finishCollect == true)
        {
            var dropoffPosition = gameObject.transform.position;
            var playerPosition = player.transform.position;
            distance = Vector3.Distance(dropoffPosition,playerPosition);

            if (distance < 3) 
            {
                uiManager.TipDropOffOn();
                tipActive = true;
            }
            else if (tipActive == true)
            {
                uiManager.TipDropOffOff();
                tipActive = false;
            }

            if (distance < 3 && playerinput.isInteracting > 0) 
            {
                finishCollect = false;
                StartCoroutine(Collect());
            }
        }
    }
    
    private IEnumerator Collect()
    {
        total = total + lootManager.totalValue;
        lootManager.totalValue = 0;
        lootManager.totalAmount = 0;
        Debug.Log(total);
        uiTotal.text = "Total: $" + total.ToString();
        finishCollect = true;
        yield return new WaitForSeconds(0.1f);
    }
}
