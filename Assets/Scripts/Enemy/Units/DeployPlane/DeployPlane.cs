using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployPlane : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Spawning parameter")]
    [SerializeField] private Transform endPoint;


    [Header("Spawn trooper")]
    [SerializeField] private GameObject trooper;
    [SerializeField] private int trooperAmount;

    [Header("Spawn shield droid")]
    [SerializeField] private GameObject shieldDroid;
    [SerializeField] private int droidAmount;

    [Header("Spawn small bot")]
    [SerializeField] private GameObject smallBot;
    [SerializeField] private int botAmount;

    private Rigidbody2D rigidbody2d;
    bool deployed = false;
  

    public void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        TriggerDeployment();
    }

    public void Update()
    {
        Vector3 direction = endPoint.position - transform.position;
        direction.Normalize();
        rigidbody2d.MovePosition((Vector2)transform.position + (Vector2)direction * 4 * Time.deltaTime);
        if (deployed)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    IEnumerator Deploy()
    {
        for (int i = 0; i < trooperAmount; i++)
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
        deployed = true;
    }

    public void TriggerDeployment()
    {
           StartCoroutine(Deploy());
    }



}
