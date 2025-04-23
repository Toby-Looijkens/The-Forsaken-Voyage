using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI uiTip;
    public GameObject canvasTitle;
    public GameObject canvasMenu;
    public GameObject canvasControls;

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

    public void ToMenu()
    {
        canvasTitle.SetActive(false);
        canvasControls.SetActive(false);
        canvasMenu.SetActive(true);
    }

    public void ToControls()
    {
        canvasTitle.SetActive(false);
        canvasMenu.SetActive(false);
        canvasControls.SetActive(true);
    }

    public void StartGame()
    {
        
    }
}
