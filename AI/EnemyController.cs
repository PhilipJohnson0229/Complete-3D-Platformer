using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DillonsDream
{
    public class EnemyController : MonoBehaviour
    {

        public Transform[] patrolPoints;
        public int currentPoint;
        public Animator anim;
        public NavMeshAgent agent;

        public float waitTime = 2f;
        public float waitCOunter;

        public float chaseRange;
        public float attackRange = 1f;
        public float timeBetweenAttacks = 1f;
        private float attackCOunter;
        public Transform playerPos;

        public bool isKnocking;
        public float knockBackLength = 0.3f;
        [SerializeField]
        private float knockBackCounter;
        public Vector2 knockBackPower;
        public enum AIStates
        {
            isIdle,
            isPatrolling,
            isHunting,
            isAttacking,
            isKnockedBack
        };

        public AIStates currentState;

        void Start()
        {
            waitCOunter = waitTime;
            knockBackCounter = knockBackLength;
        }

        // Update is called once per frame
        void Update()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
            switch (currentState)
            {
                case AIStates.isIdle:

                    anim.SetBool("IsMoving", false);

                    if (waitCOunter > 0)
                    {
                        waitCOunter -= Time.deltaTime;
                    }
                    else
                    {
                        currentState = AIStates.isPatrolling;
                        agent.SetDestination(patrolPoints[currentPoint].position);
                       // Debug.Log("Here we go!");
                    }

                    if (distanceToPlayer <= chaseRange)
                    {
                        currentState = AIStates.isHunting;
                    }
                    knockBackCounter = knockBackLength;

                    break;

                case AIStates.isPatrolling:

                    if (agent.remainingDistance <= 0.2f)
                    {
                        currentPoint++;
                        if (currentPoint >= patrolPoints.Length)
                        {
                            currentPoint = 0;
                        }

                        currentState = AIStates.isIdle;
                        waitCOunter = waitTime;
                    }

                    if (distanceToPlayer <= chaseRange)
                    {
                        currentState = AIStates.isHunting;
                    }

                    anim.SetBool("IsMoving", true);

                    break;

                case AIStates.isHunting:

                    agent.SetDestination(PlayerController.instance.transform.position);
                    if (distanceToPlayer <= attackRange)
                    {
                        currentState = AIStates.isAttacking;
                        anim.SetTrigger("Attack");
                        anim.SetBool("IsMoving", false);
                        //this will stop the navmesh from moving
                        agent.velocity = Vector3.zero;
                        agent.isStopped = true;
                        attackCOunter = timeBetweenAttacks;
                    }
                    else
                    {
                        anim.SetBool("IsMoving", true);
                    }

                    if (distanceToPlayer > chaseRange)
                    {
                        currentState = AIStates.isIdle;
                        waitCOunter = waitTime;
                        agent.velocity = Vector3.zero;
                        agent.SetDestination(transform.position);
                    }

                    break;

                case AIStates.isAttacking:

                    transform.LookAt(PlayerController.instance.transform, Vector3.up);
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                    attackCOunter -= Time.deltaTime;
                    if (attackCOunter <= 0)
                    {
                        if (distanceToPlayer < attackRange)
                        {
                            anim.SetTrigger("Attack");
                            attackCOunter = timeBetweenAttacks;
                        }
                        else
                        {
                            currentState = AIStates.isIdle;
                            waitCOunter = waitTime;
                            agent.isStopped = false;
                        }
                    }

                    break;

                case AIStates.isKnockedBack:

                    knockBackCounter -= Time.deltaTime;

                    if (knockBackCounter <= 0)
                    {
                        isKnocking = false;
                        currentState = AIStates.isIdle;
                        agent.ResetPath();
                    }
                    float moveSpeed = 2f;
                    this.transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

                    waitCOunter = waitTime;
                    break;
            }

        }

        public void KnockBack()
        {
            anim.SetTrigger("Damaged");
            isKnocking = true;
            currentState = AIStates.isKnockedBack;
            knockBackCounter = knockBackLength;
            Debug.Log("Punched that bitch in the face");
        }
    }
}


