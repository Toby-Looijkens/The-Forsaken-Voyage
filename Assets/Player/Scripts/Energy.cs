using UnityEngine;

public class Energy : MonoBehaviour
{
    
    public float energy = 100;
    public UIBars uiBars;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseEnergy(float decreaseAmount)
    {
        energy = energy - decreaseAmount;
        if (energy < 0)
        {
            energy = 0;
        }
        uiBars.EnergyBar(energy / 100);
    }
}
