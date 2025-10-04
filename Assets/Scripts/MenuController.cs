using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    public GameObject pauseMenu;
    public TMP_Text Name;
    public string playerName = "player";

    private void Awake()
    {
        Instance = this;
        
        playerName = MainMenuController.currentPlayerName;
    }


    void Start()
    {
        pauseMenu.SetActive(false);
        Name.text = "PLAYER: "+ playerName;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void RestartGame() { SceneManager.LoadScene("MainMenu"); }



    public void EndGame() { Application.Quit(); }
}

