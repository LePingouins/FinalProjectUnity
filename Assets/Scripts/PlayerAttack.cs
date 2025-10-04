


using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private PlayerAttackHitbox attackHitbox; // Corrigé : on utilise le script, pas un GameObject

    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        cooldownTimer = 0;

        // Active la hitbox via son script
        attackHitbox.EnableHitbox();
    }
}


/*
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;


    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity; //Player will not be able to attack straight away

    //Initializes the animator and playerMovement variables
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()) //Checks if the left mouse button is pressed and the cooldown timer is greater than the attack cooldown or if enough time has passed to fire another attack
            Attack();

        cooldownTimer += Time.deltaTime; //Increments the cooldown timer by the time that has passed since the last frame
    }
    private void Attack()
    {
        animator.SetTrigger("attack"); //Sets the attack trigger in the animator
        cooldownTimer = 0; //Resets the cooldown timer

        //pool fireballs


        fireballs[FindFireBall()].transform.position = firePoint.position;
        fireballs[FindFireBall()].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }    
    private int FindFireBall()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

}
*/