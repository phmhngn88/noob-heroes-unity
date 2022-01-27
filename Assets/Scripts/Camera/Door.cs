using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    [Header("Check enemy")]
    [SerializeField] private float range;
    [SerializeField] private GameObject[] traps;

    [SerializeField] private Transform deactiveObject;
    private GameObject enemyLeft;

    private void Start()
    {
        deactiveObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        enemyLeft = GameObject.FindGameObjectWithTag("GroundUnit");
        if (enemyLeft == null)
            enemyLeft = GameObject.FindGameObjectWithTag("Enemy");
        if(enemyLeft == null)
        {
            //Debug.Log("is trigger now");
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
            GetComponent<BoxCollider2D>().isTrigger = false;


        if (deactiveObject.position.x > transform.position.x) {
            GetComponent<BoxCollider2D>().isTrigger = false;
            foreach(GameObject obj in traps)
            {
                obj.SetActive(false);
            }
            this.enabled = false;
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log(collision.gameObject);
            Debug.Log("Wronghere");
            if (collision.transform.position.x < transform.position.x)
                cam.MoveToNewArea(nextRoom);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
