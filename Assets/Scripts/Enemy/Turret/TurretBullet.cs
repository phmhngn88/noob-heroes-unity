using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float resetTime;
    private float lifetime;

    // Start is called before the first frame update
    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
              collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
