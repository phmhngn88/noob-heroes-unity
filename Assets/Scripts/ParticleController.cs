using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    new ParticleSystem particleSystem;
    float[] bombDamage = { 1, 3, 6 };

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }

    public void Explode()
    {
        particleSystem.Play();
        gameObject.GetComponent<Renderer>().enabled = false; // Hide oject
        gameObject.GetComponent<BoxCollider2D>().enabled = false; // Disable object
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(gameObject.name);
        particleSystem.Play();
        if (gameObject.name.Equals("land_mine(Clone)") == false)
        {
            gameObject.GetComponent<Renderer>().enabled = false; // Hide oject
            gameObject.GetComponent<BoxCollider2D>().enabled = false; // Disable object

            //test damage - Cuong
            if (collision.collider.GetComponent<UnitHealth>() != null)
            {
                Debug.Log(collision.collider.gameObject);
                collision.collider.GetComponent<UnitHealth>().TakeDamage(bombDamage[BowController.currentArrow]);
                //Debug.Log(bombDamage[BowController.currentArrow]);
            }
        }
        //StartCoroutine(Count1Second());
    }

    IEnumerator Count1Second()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return bombDamage[BowController.currentArrow];
    }    
}
