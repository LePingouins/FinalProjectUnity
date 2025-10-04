using UnityEngine; 

public class PlayerAttackHitbox : MonoBehaviour
{
    private bool isAttacking = false; 

    // Called when the player performs an attack
    public void EnableHitbox()
    {
        isAttacking = true;             // Enable attack state
        gameObject.SetActive(true);     // Activate the hitbox GameObject
        Invoke(nameof(DisableHitbox), 0.2f); 
    }

    // Disables the attack hitbox
    private void DisableHitbox()
    {
        isAttacking = false;            // Disable attack state
        gameObject.SetActive(false);    // Hide/deactivate the hitbox GameObject
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag("Enemy"))
        {
            Debug.Log("Ennemi touched!"); 
            AudioManager.Instance.PlaySFX("Hit"); 

            HeavyBandit enemy = other.GetComponent<HeavyBandit>(); 
            if (enemy != null)
            {
                enemy.TakeDamage(); // Deal damage to the enemy
            }
        }
    }
}
