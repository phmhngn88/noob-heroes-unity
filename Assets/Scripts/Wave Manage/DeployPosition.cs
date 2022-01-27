using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployPosition : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform triggerObject;
    [SerializeField] private LayerMask playerLayer;
    private bool detected = false;

    private Vector3 direction;
    private void Start()
    {
        triggerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 targetPos = triggerObject.position;
        direction = targetPos - transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, range, playerLayer);

        if (rayInfo)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
