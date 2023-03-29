using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MQSAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private PlayerHealth playerHealth;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private float attackRange = 5;
    private bool canAttack = true;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerInView());
        if(PlayerInView())
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        } 
        else
        {
            agent.isStopped = true;
        } 

        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange && canAttack) Attack();
    }

    bool PlayerInView()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, 50, layerMask);
        

        if (hit.transform == null) return false;
        if (hit.transform.tag == player.tag) return true;
        
        Debug.Log("Hit was not null but obj hit is not player");
        
        return false;
    }

    void Attack()
    {
        playerHealth.HealthDecrease(40);
        canAttack = false;
        Invoke(nameof(AttackReset), 2f);
    }

    void AttackReset()
    {
        canAttack = true;
    }
}
