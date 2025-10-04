using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
  

    public TMP_InputField playerNameInput;
    public TMP_Text highScoresText;
    public static string currentPlayerName = "Player";


    void Start()
    {
        
    }

    public void StartNewGame()
    {
        currentPlayerName = playerNameInput.text != "" ? playerNameInput.text : "Player";
        Debug.Log($"Starting new game with player name: {currentPlayerName}");

        // Set the player name in GameManager (if it exists in the scene)
        if (MenuController.Instance != null)
        {
            MenuController.Instance.playerName = currentPlayerName;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1 1");


    }

    public void SaveGame()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }



    public void EndGame()
    {
        {
            Debug.Log("Exiting game...");

#if UNITY_EDITOR
            // This works only in the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
#else
        // This works in builds
        Application.Quit();
#endif
        }
    }
}
