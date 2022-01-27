using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCollectible : MonoBehaviour
{
    [SerializeField] private GameObject pet;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject petIns = Instantiate(pet, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
