using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask playerLayer;
    private bool detected = false;
 
    private Vector3 direction;
    private Rigidbody2D gunRigidbody2d;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float FireRate;
    [SerializeField] private float nextFireTime = 0;
    public Transform shootPoint1;
    public Transform shootPoint2;
    [SerializeField] private float Force;
    private Animator anim;
    private void Start()
    {
        gunRigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 targetPos = target.position;
        direction = targetPos - transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, range, playerLayer);

        if(rayInfo)
        {
            detected = true;        
        }
        else
            detected = false;

        if (detected)
        {
            //Debug.Log("detected");
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gunRigidbody2d.rotation = angle;
            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + 1 / FireRate;
                
            }
            anim.SetBool("shoot", true);
        }
        else
            anim.SetBool("shoot", false);

    }

    void Shoot()
    {
        GameObject BulletIns1 = Instantiate(bullet, shootPoint1.position, Quaternion.identity);
        GameObject BulletIns2 = Instantiate(bullet, shootPoint2.position, Quaternion.identity);
        BulletIns1.GetComponent<Rigidbody2D>().AddForce(direction * Force);
        BulletIns2.GetComponent<Rigidbody2D>().AddForce(direction * Force);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void AfterDie()
    {
        Destroy(gameObject);
    }
}
