using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    NavMeshAgent myAgent;
    public LayerMask whatIsGround, whatIsPlayer;

    public Transform player;
    Animator animator;

    public Transform firePosition;

    //Guarding some field
    public Vector3 destinationPoint;
    bool destinationSet;
    public float destinationRange;

    //Chasing player inside of range
    public float chaseRange;
    bool playerInChaseRange;

    //Enemy attacking
    public float attackRange, attackTime;
    private bool playerInAttackRange, readyToAttack = true;
    public GameObject attackProjectile;

    //Melee attack
    public bool meleeAttacker;


    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        myAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInChaseRange && !playerInAttackRange)
            Guarding();
        if (playerInChaseRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInChaseRange && playerInAttackRange)
            AttackPlayer();
    }

    private void AttackPlayer()
    {
        myAgent.SetDestination(transform.position);
        transform.LookAt(player);


        if (readyToAttack && !meleeAttacker)
        {
            animator.SetTrigger("Attack");


            firePosition.LookAt(player);
            Instantiate(attackProjectile, firePosition.position, firePosition.rotation);

            readyToAttack = false;
            StartCoroutine(ResetAttack());
        } else if(readyToAttack && meleeAttacker)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void MeleeDamage()
    {
        if (playerInAttackRange)
        {
            //Reduce player health
        }
    }

    private void ChasePlayer()
    {
        myAgent.SetDestination(player.position);
    }

    private void Guarding()
    {
        if (!destinationSet)
        {
            SearchForDestination();
        }
        else
        {
            myAgent.SetDestination(destinationPoint);
        }

        Vector3 distanceToDestination = transform.position - destinationPoint;
        if (distanceToDestination.magnitude < 1f)
        {
            destinationSet = false;
        }
    }

    private void SearchForDestination()
    {
        //random point for our AI Enemy 
        float randPositionZ = UnityEngine.Random.Range(-destinationRange, +destinationRange);
        float randPositionX = UnityEngine.Random.Range(-destinationRange, +destinationRange);

        destinationPoint = new Vector3(
            transform.position.x + randPositionX,
            transform.position.y,
            transform.position.z + randPositionZ);

        if (Physics.Raycast(destinationPoint, -transform.up, 2f, whatIsGround))
        {
            destinationSet = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackTime);
        readyToAttack = true;
    }

}
