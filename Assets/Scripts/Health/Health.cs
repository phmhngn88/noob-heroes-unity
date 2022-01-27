using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } //other script can get health but only set this script
    private Animator animator;
    private bool isDead;
    // Start is called before the first frame update

    bool invicible;
    void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        if (!invicible) { 
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
            if (currentHealth > 0 )
            {
                //hurt
                animator.SetTrigger("hurt");
            
                StartCoroutine(Invunerability());
                //iframe

            }
            else
            {
                //die
                if (!isDead)
                {
                    animator.SetTrigger("die");

                    PlayerController controller = GetComponent<PlayerController>();
                    if (controller != null)
                        controller.enabled = false;
                    isDead = true;
                }
            }
        }
    }

    public void AddHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
    }

    public void SetHealth(float amount)
    {
        currentHealth = Mathf.Clamp(amount, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        invicible = true;
        yield return new WaitForSeconds(1f);
        invicible = false;
    }
}
