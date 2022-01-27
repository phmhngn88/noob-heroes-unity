using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } //other script can get health but only set this script
    private Animator animator;
    //private bool isDead;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        //isDead = false;
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth == 0)
        {
                animator.SetTrigger("die");
        }
    }
}
