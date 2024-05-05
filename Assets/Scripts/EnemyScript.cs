using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;

        if(agent.velocity.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                animator.SetBool("isAttacking", true);
            }
        } else
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
