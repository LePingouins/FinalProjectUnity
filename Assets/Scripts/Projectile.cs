using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private bool hit;
    private float direction;
    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Animator animator;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //If the projectile has hit something, return
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        //Move the projectile to the right
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 5)
            gameObject.SetActive(false);
            //Deactivate();
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("explode");
    }
    public void setDirection(float _direction)
    {
        //If the direction is less than 0, flip the projectile sprite
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        //if (direction < 0)
        //    transform.localScale = new Vector3(-1, 1, 1);
        //else
        //    transform.localScale = Vector3.one;

        //float localScaleX = Mathf.Abs(transform.localScale.x);
        //transform.localScale = new Vector3(localScaleX * direction, transform.localScale.y, transform.localScale.z);

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
