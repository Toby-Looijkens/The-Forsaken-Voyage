using UnityEngine;

public class MusicSwitch : MonoBehaviour
{

    public GameObject ambience;
    public GameObject combat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SwapToCombatOST()
    {
        ambience.SetActive(false);
        combat.SetActive(true);
    }

    public void SwapToAmbienceOST()
    {
        combat.SetActive(false);
        ambience.SetActive(true);
    }
}
