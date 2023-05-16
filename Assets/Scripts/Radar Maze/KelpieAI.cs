using UnityEngine;
using UnityEngine.AI;

public class KelpieAI : MonoBehaviour
{
    // Objects
    [SerializeField] private NavMeshAgent kelpie;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] keyAreas;

    // Kelpie Settings
    [SerializeField] private float kelpieRange = 30;
    private NavMeshAgent agent;


    private Vector3 lastPlayerPos;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < kelpieRange)
        {
            kelpie.SetDestination(player.transform.position);
            lastPlayerPos = player.transform.position;
        }
        else
        {
            Hunt();
        }
    }

    void Hunt()
    {
        if (Vector3.Distance(kelpie.destination, transform.position) > 10) return;

        Vector3 destination;
        switch (Random.Range(1, keyAreas.Length))
        {
            case 1:
                destination = keyAreas[0].transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                kelpie.SetDestination(destination);
                break;

            case 2:
                destination = keyAreas[1].transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                kelpie.SetDestination(destination);
                break;

            case 3:
                destination = keyAreas[2].transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                kelpie.SetDestination(destination);
                break;

            default:
                Debug.Log("Fuck did you do lol Kelpie Default switchy thing");
                break;
        }
    }

    public void IncreaseDiff()
    {
        Debug.Log("Action is coming (African accent)");

        agent.speed *= 1.1f;
        kelpieRange += 10;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RadarGameManager.Instance.Death();
        }
    }
}
