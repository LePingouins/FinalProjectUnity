using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HouseEntrance : MonoBehaviour
{
    public string interiorSceneName = "Level1"; // Name of the interior scene
    public GameObject enterPromptUI; // Drag your EnterPrompt UI here

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(interiorSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            enterPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            enterPromptUI.SetActive(false);
        }
    }
}
