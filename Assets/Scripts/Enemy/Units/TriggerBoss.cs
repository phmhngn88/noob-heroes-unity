using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    [Header("Spawning parameter")]
    [SerializeField] private float range;
    [SerializeField] private Transform triggerObject;
    [SerializeField] private LayerMask playerLayer;
    private Vector3 direction;
    // Start is called before the first frame update

    void Start()
    {
        triggerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = triggerObject.position;
        direction = targetPos - transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, range, playerLayer);

        if (rayInfo)
        {
            Debug.Log("enble");
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    

}
