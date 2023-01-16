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

    //Guarding some field
    public Vector3 destinationPoint;
    bool destinationSet;
    public float destinationRange;

    //Chasing player inside of range
    public float chaseRange;
    bool playerInChaseRange;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);

        if (!playerInChaseRange)
            Guarding();
        else if(playerInChaseRange)
            ChasePlayer();

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
    }
}
