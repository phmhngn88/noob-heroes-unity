using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interceptor : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float delayTime;
    [SerializeField] private float damage;
    private Rigidbody2D rigidbody2d;
    private Animator anim;
    private Vector2 movement;

    bool CR_running;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void ActivateInterceptor(Transform dir)
    {
        target = dir;
        Attack();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rigidbody2d.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        if (!CR_running)
            MoveInterceptor(movement);
    }

    void MoveInterceptor(Vector2 direction)
    {
        rigidbody2d.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.deltaTime);
    }

    IEnumerator WaitForCharge()
    {
        CR_running = true;
        yield return new WaitForSeconds(delayTime);
        CR_running = false;
    }

    public void Attack()
    {
        StartCoroutine(WaitForCharge());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Health>().TakeDamage(damage);
            anim.SetTrigger("die");
        }
    }

    public void Explode()
    {
        Destroy(gameObject);
    }

    public void StartExplode()
    {
        gameObject.layer = LayerMask.NameToLayer("Arrow");
    }

}
