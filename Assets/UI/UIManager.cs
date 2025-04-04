using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI uiTip;
    public TextMeshProUGUI uiAmmo;
    public Hitscan hitscan;
    public int ammo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammo = hitscan.ammo;
        uiAmmo.text = "Ammo: " + ammo.ToString();
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
}
