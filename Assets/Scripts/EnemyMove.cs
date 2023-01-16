using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Animator myAnimator;
    Transform player;

    public bool move, rotate;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;

        myAnimator.SetBool("Move", move);
        myAnimator.SetBool("Rotating", rotate);
    }

    void Update()
    {
        transform.LookAt(player);
    }
}
