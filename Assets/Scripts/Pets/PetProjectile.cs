using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float damage;
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Vector3 direction;

    public void ActivateProjectile(Vector3 direction)
    {
        lifetime = 0;
        gameObject.SetActive(true);
        this.direction = direction;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag != "Player")
        {
            UnitHealth health = collision.GetComponent<UnitHealth>();
            if (health != null)
            {
                Debug.Log(collision);
                health.TakeDamage(damage);
                
            }
            Destroy(gameObject);
        }
    }
}
