using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MQSAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    [SerializeField] private LayerMask layerMask;

    private float attackRange = 10;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerInView());
        if(PlayerInView()) agent.SetDestination(player.transform.position);
        else agent.isStopped = true;

        if (Vector3.Distance(player.transform.position, transform.position) >= attackRange) Attack();
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
        // Damage player
    }
}
