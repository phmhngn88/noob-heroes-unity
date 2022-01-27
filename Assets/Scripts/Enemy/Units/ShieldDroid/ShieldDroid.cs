using System.Collections;
using UnityEngine;

public class ShieldDroid : MonoBehaviour
{
    [Header("Shield parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private GameObject shield;
    [SerializeField] private float nextShield; //should be 0.0x 

    [Header("Collider parameter")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Enemy layer")]
    [SerializeField] private LayerMask enemyLayer;
    private float cooldownTimer = Mathf.Infinity;
    private float durationTimer = Mathf.Infinity;

    //
    public Transform target;
    private SpriteRenderer spriteRenderer;
    //ref
    private Animator animator;
    // Start is called before the first frame update
    public void Awake()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("GroundUnit").GetComponent<Transform>();
        //Debug.Log(target);
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (EnemyInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetBool("running", false);
                animator.SetTrigger("shield"); //active shield
                //animator.SetBool("shielding", true);
                durationTimer += Time.deltaTime;
                //shield.SetActive(true);
                if (durationTimer >= nextShield) //shielding 
                {
                    durationTimer = 0;
                    shield.SetActive(false);
                    animator.SetBool("shielding", false);
                }
            }
        }
        else
        {
            durationTimer = 0;
            animator.ResetTrigger("shield");
            shield.SetActive(false);
            animator.SetBool("shielding", false);
            //
            GetComponent<Rigidbody2D>().isKinematic = false;
        }


        if (target != null && !EnemyInSight() && !animator.GetBool("shielding")) //move till player in attack range
        {
            animator.SetBool("running", true);

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), 2
                * Time.deltaTime);
            this.spriteRenderer.flipX = target.transform.position.x < this.transform.position.x;
        }
        else
        {
            GameObject newTarget = GameObject.FindGameObjectWithTag("GroundUnit");
            if (newTarget != null)
                target = newTarget.GetComponent<Transform>();
        }


    }

    private bool EnemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right
            * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

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

    public void AfterDie()
    {
        MyManager.Instance.coin++;
        Destroy(gameObject);
    }

    public void StartDying()
    {
        gameObject.layer = LayerMask.NameToLayer("Arrow");
    }

    public void SetShielding()
    {
        animator.SetBool("shielding", true);
        shield.SetActive(true);
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
