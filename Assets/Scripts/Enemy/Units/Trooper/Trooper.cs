using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trooper : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;

    [Header("Ranged attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform gunEndPoint;
    [SerializeField] private GameObject bullet;


    [Header("Collider parameter")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    public Transform target;
    private Animator animator;
    private Vector3 initScale;
    // Start is called before the first frame update
    public void Awake()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        initScale = transform.localScale;
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
                animator.SetBool("moving", false);
                animator.SetTrigger("attack");
            }
        }

        if (target != null && !PlayerInSight()) //move till player in attack range
        {
            animator.SetBool("moving", true);

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), 2
                * Time.deltaTime);
            if (target.transform.position.x > this.transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(initScale.x) * 1, initScale.y, initScale.z);
            }
            else
                transform.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y, initScale.z);
        }

    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right
            * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);


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

    private void RangedAttack()
    {
        cooldownTimer = 0;
        GameObject bulletIns = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Vector3 shootDir = (gunEndPoint.position - firePoint.position).normalized;
        bulletIns.GetComponent<TrooperProjectile>().ActivateProjectile(shootDir);
    }

    public void AfterDie()
    {
        MyManager.Instance.coin++;

        Destroy(gameObject);
    }

    public void StartDying()
    {
        gameObject.layer = LayerMask.NameToLayer("Arrow");
    }
}
