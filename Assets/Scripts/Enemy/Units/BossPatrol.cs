using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform[] patrolPoints;
    private int currentPointIndex = 0; 

    [Header("Boss")]
    [SerializeField] private Transform enemy;
    private Rigidbody2D rigidbody2d;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;


    [Header("Idle behavior")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private void Awake()
    {
        initScale = enemy.localScale;
        rigidbody2d = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Vector2.Distance(enemy.position, patrolPoints[currentPointIndex].position) < 0.2f)
        {
            DirectionChange();
        }
        else
        {
            int faceDir = 1;
            Vector3 direction = patrolPoints[currentPointIndex].position - enemy.position;
            direction.Normalize();
            if (direction.x > 0)
                faceDir = -1;
            MoveInDirection(direction, faceDir);
        }
    }


    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration) {
            if (currentPointIndex != patrolPoints.Length - 1)
                currentPointIndex++;
            else
                currentPointIndex = 0;
            GetComponentInChildren<DogHeadBoss>().Attack();
        }
            
    }

 
    private void MoveInDirection(Vector2 direction, int faceDir)
    {
        idleTimer = 0;
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * faceDir, initScale.y, initScale.z);
        rigidbody2d.MovePosition((Vector2)enemy.position + direction * speed * Time.deltaTime);
    }
}
