using UnityEngine;
using UnityEngine.UI;

public class UIBars : MonoBehaviour
{
    
    public Image fillHP;
    public Image fillEnergy;
    public Image fillWeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealthBar (float hp)
    {
        fillHP.fillAmount = Mathf.Clamp01(hp);
    }

    public void EnergyBar (float energy)
    {
        fillEnergy.fillAmount = Mathf.Clamp01(energy);
    }

    public void WeightBar (float weight)
    {
        fillWeight.fillAmount = Mathf.Clamp01(weight);
    }
}
