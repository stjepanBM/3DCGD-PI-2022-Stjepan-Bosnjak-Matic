using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    NavMeshAgent myAgent;
    public LayerMask whatIsGround;

    public Vector3 destinationPoint;
    bool destinationSet;
    public float destinationRange;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Guarding();
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
}
