using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrooperPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;

    [Header("Idle behavior")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy animator")]
    [SerializeField] private Animator animator;

    private bool movingLeft;

    // Start is called before the first frame update
    private void Awake()
    {
        initScale = enemy.localScale;

    }
    // Update is called once per frame
    void Update()
    {
        if(movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
            {
                DirectionChange();
            }

        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
            {
                DirectionChange();
            }
        }
    }

    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }
    private void DirectionChange()
    {

        animator.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }
    private void MoveInDirection(int dir)
    {
        idleTimer = 0;
        animator.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * dir, initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * dir * speed,
            enemy.position.y, enemy.position.z);
    }
}
