using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public GameObject canvasTitle;
    public GameObject canvasMenu;
    public GameObject canvasControls;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasControls.SetActive(false);
        canvasMenu.SetActive(false);
        canvasTitle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene("TutorialMap");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit game");
    }
}
