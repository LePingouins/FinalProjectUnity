using UnityEngine; 
using UnityEngine.SceneManagement; 

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; 

    private int currentHealth;      
    private Animator animator;      
    private bool isDead = false;    // Used to prevent actions after the player dies

    private void Start()
    {
        currentHealth = maxHealth;               // Initialize the player's health to the maximum
        animator = GetComponent<Animator>();     
    }

    public void TakeDamage()
    {
        if (isDead) return; // If the player is already dead, ignore further damage

        currentHealth--; // Reduce health by 1
        Debug.Log("Player touched! HP left: " + currentHealth); 

        if (currentHealth > 0)
        {
            animator.Play("Hurt"); 
        }
        else
        {
            isDead = true; // Mark the player as dead
            AudioManager.Instance.PlaySFX("Death"); 
            animator.Play("Death"); 
            Debug.Log("The player is dead. Returning to the main menu...");

            Invoke("LoadMainMenu", 2f); 
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
