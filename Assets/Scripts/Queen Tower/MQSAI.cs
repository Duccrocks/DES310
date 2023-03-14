using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MQSAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    [SerializeField] private LayerMask layerMask;

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
    }

    bool PlayerInView()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, player.transform.position, Color.green);
        Physics.Raycast(transform.position, player.transform.position, out hit, 500, layerMask);
        

        if (hit.transform == null) return false;
        if (hit.transform.tag == player.tag) return true;
        
        Debug.Log("Hit was not null but obj hit is not player");
        
        return false;
    }
}
