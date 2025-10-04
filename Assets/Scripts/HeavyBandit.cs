using UnityEngine; 

public class HeavyBandit : MonoBehaviour
{
    public Transform player;           
    public float followRange = 5f;     
    public float attackRange = 1.2f;  
    public float moveSpeed = 2f;       

    private Animator animator;         
    private Rigidbody2D rb;            
    private int hitPoints = 2;         
    private bool isDead = false;       

    private void Start()
    {
        animator = GetComponent<Animator>();       
        rb = GetComponent<Rigidbody2D>();          
    }

    private void Update()
    {
        if (player == null || isDead) return;      // Do nothing if there's no player or if the bandit is already dead

        float distance = Vector2.Distance(transform.position, player.position); // Calculate distance to player
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0f).normalized; // Horizontal direction to the player

        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;      // Stop moving when attacking
            animator.Play("Attack");               // Play attack animation

            // Try to get the PlayerHealth component and inflict damage
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();         // Call damage method on the player
            }
        }
        else if (distance <= followRange)
        {
            rb.linearVelocity = direction * moveSpeed; // Move towards the player

            // Only play "Run" animation if not already playing
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                animator.Play("Run");
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;      // Stop moving if player is out of range
            animator.Play("Idle");                 // Play idle animation
        }
    }

    public void TakeDamage()
    {
        if (isDead) return;           // Ignore if already dead

        hitPoints--;                  // Reduce hit points

        if (hitPoints > 0)
        {
            animator.Play("Hurt");    // Play hurt animation if still alive
        }
        else
        {
            isDead = true;                            // Mark as dead to stop future behavior
            rb.linearVelocity = Vector2.zero;         // Stop all movement
            animator.Play("Death");                   // Play death animation
            Destroy(gameObject, 1.5f);                // Destroy the bandit after 1.5 seconds
        }
    }
}
