using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;

    public Rigidbody myRigidBody;

    void Start()
    {
        
    }

    void Update()
    {
        BulletMovement();
    }

    private void BulletMovement()
    {
        myRigidBody.velocity = transform.forward * speed;
    }
}
