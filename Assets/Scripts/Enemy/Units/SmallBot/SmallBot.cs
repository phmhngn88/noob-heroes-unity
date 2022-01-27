using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBot : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header("Collider parameter")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    //
    public Transform target;
    private SpriteRenderer spriteRenderer;
    //ref
    private Health playerHealth;
    private Animator animator;
    // Start is called before the first frame update
    public void Awake()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetBool("running", false);
                animator.SetTrigger("attack"); //attack, damage is in attack event
            }
        }

        if (target != null && !PlayerInSight()) //move till player in attack range
        {
            animator.SetBool("running", true);

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), 2
                * Time.deltaTime);
            this.spriteRenderer.flipX = target.transform.position.x < this.transform.position.x;
        }

    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right
            * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();

        }



        return hit.collider != null;
    }

    //draw collider box that check player in sight
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range *
            transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
    public void StartDying()
    {
        gameObject.layer = LayerMask.NameToLayer("Arrow");
    }
    public void AfterDie()
    {
        MyManager.Instance.coin++;
        Destroy(gameObject);
    }


}
