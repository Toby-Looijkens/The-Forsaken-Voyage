using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI uiTip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
