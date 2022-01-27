using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrooperProjectile : EnemyDamage
{
    // Start is called before the first frame update
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
        if(lifetime > resetTime)
        {
           Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //automatically access parent method first
        gameObject.SetActive(false); //deactivate when hit something
    }
}
