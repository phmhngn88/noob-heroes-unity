using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Spawning parameter")]
    [SerializeField] private float range;
    [SerializeField] private Transform triggerObject;
    [SerializeField] private LayerMask playerLayer;
    private Vector3 direction;

    [Header("Spawn trooper")]
    [SerializeField] private GameObject trooper;
    [SerializeField] private int trooperAmount;

    [Header("Spawn shield droid")]
    [SerializeField] private GameObject shieldDroid;
    [SerializeField] private int droidAmount;

    [Header("Spawn small bot")]
    [SerializeField] private GameObject smallBot;
    [SerializeField] private int botAmount;

    //private bool detected = false;
    bool CR_running;

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
            TriggerSpawner();
        }

    }

    IEnumerator Spawn()
    {
        CR_running = true;
        
        for (int i = 0; i < trooperAmount;i++)
        {
           Instantiate(trooper, transform.position, Quaternion.identity);
           yield return new WaitForSeconds(1f);
        }
        for (int i = 0; i < droidAmount; i++)
        {
            Instantiate(shieldDroid, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
        for (int i = 0; i < botAmount; i++)
        {
            Instantiate(smallBot, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
        
        CR_running = false;
        gameObject.SetActive(false);
    }

    public void TriggerSpawner()
    {
        if (!CR_running)
            StartCoroutine(Spawn());
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
