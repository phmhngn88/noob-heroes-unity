using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    new ParticleSystem particleSystem;
    Rigidbody2D rb2d;
    bool hasHit;
    // Start is called before the first frame update
    void Start()
    {
        rb2d.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasHit == false)
        {
            float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    void OnCollisionEnter2D(Collision2D collision) //Va chạm sẽ đém thời gian rồi hủy vật bom đi.
    {
        hasHit = true;
        particleSystem.Play();
        Debug.Log(gameObject + "asasasas");
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.SetActive(false);
        StartCoroutine(Count1Second());
    }

    IEnumerator Count1Second()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
