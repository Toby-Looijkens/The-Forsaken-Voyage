using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI uiTip;
    public TextMeshProUGUI uiAmmo;
    public TextMeshProUGUI uiTotal;
    public TextMeshProUGUI uiHolding;
    public Hitscan hitscan;
    public int ammo;
    public int reserveAmmo;
    private int uiManQuota;
    private int totalCollected;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitscan.isReloading) 
        {
            uiAmmo.text = "Reloading...";
        }
        else
        {
            ammo = hitscan.ammo;
            reserveAmmo = hitscan.reserveAmmo;
            uiAmmo.text = ammo.ToString() + "/" + reserveAmmo.ToString();
        }
        
        if (totalCollected >= uiManQuota)
        {
            SceneManager.LoadScene("Victory");
        }
        
    }

    public void TipLootCollectOn()
    {
        uiTip.text = "[F] to collect";
    }

    public void TipLootCollectOff()
    {
        uiTip.text = "";
    }

    public void TipDropOffOn()
    {
        uiTip.text = "[F] to drop off loot";
    }

    public void TipDropOffOff()
    {
        uiTip.text = "";
    }

    public void SetQuotaUI(int quota)
    {
        uiManQuota = quota;
        UpdateTotal(0);
    }

    public void UpdateTotal(int uiManTotal)
    {
        uiTotal.text = "$" + uiManTotal + " / $" + uiManQuota;
        totalCollected = uiManTotal;
    }

    public void UpdateHolding (int uiManHolding)
    {
        uiHolding.text = "Holding: $" + uiManHolding;
    }
}
