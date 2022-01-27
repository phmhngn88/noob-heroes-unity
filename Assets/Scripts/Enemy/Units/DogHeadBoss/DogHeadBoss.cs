using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogHeadBoss : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject fighter;

    [Header("Fighters spawn points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Player layer")]
    private float cooldownTimer = Mathf.Infinity;
    private Transform target;

    //modify later
    bool CR_running;
    private BossPatrol bossPatrol;
    // Start is called before the first frame update
    public void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bossPatrol = GetComponentInParent<BossPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
        }

        //Stop patrolling and attack
        if (bossPatrol != null)
        {
            bossPatrol.enabled = !CR_running;
        }
    }


    //draw collider box that check player in sigh

    IEnumerator SpawnFighters()
    {
        cooldownTimer = 0;
        CR_running = true;
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject fighterIns = Instantiate(fighter, spawnPoint.position, Quaternion.identity);
            fighterIns.GetComponent<Interceptor>().ActivateInterceptor(target);
            yield return new WaitForSeconds(spawnDelay);
        }
        CR_running = false;
    }

    public void Attack()
    {
        if (!CR_running)
            StartCoroutine(SpawnFighters());
    }
    public void AfterDie()
    {
        transform.parent.gameObject.SetActive(false); 
    }

}
