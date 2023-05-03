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

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }



    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.TryGetComponent<PlayerHealth>(out var componentPlayerHealth))
        {
            playerHealth = componentPlayerHealth;
        }
        else
        {
            Debug.LogError("Player Health null in Knight");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerInView());
        if(PlayerInView())
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            animator.SetTrigger("EnemyWalk");
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
        if (hit.transform.CompareTag(player.tag)) return true;
        
//        Debug.Log("Hit was not null but obj hit is not player");
        
        return false;
    }

    void Attack()
    {
        animator.SetTrigger("EnemyAttack");
        playerHealth.HealthDecrease(50);
        canAttack = false;
        Invoke(nameof(AttackReset), 2f);
    }

    void AttackReset()
    {
        canAttack = true;
    }

}
