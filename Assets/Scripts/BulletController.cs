using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed, bulletLife;

    public Rigidbody myRigidBody;

    void Start()
    {

    }

    void Update()
    {
        BulletMovement();

        bulletLife -= Time.deltaTime;

        if (bulletLife < 0)
        {
            Destroy(gameObject);
        }
    }

    private void BulletMovement()
    {
        myRigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Enemy"))
        //{
        //    Destroy(other.gameObject);
        //}

        Destroy(gameObject);
    }

}
