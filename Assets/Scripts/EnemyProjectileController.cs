using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{

    Rigidbody myRigidBody;
    public float upForce, forwardForce;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();

        GrenadeThrow();
    }

    private void GrenadeThrow()
    {
        myRigidBody.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        myRigidBody.AddForce(transform.up * upForce, ForceMode.Impulse);
    }

    void Update()
    {
        
    }
}
