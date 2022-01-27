using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), 2
                * Time.deltaTime);
            //transform.LookAt(target.position, Vector3.back);
            this.spriteRenderer.flipX = target.transform.position.x > this.transform.position.x;
        }
    }
}
