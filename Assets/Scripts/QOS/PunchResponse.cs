using UnityEngine;
using UnityEngine.AI;

public class PunchResponse : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int health = 3;
    [SerializeField] private float IFrameTime = 2;

    private Vector3 direction;

    private float healthTimer;
    private NavMeshAgent navMeshAgent;

    private RaddollManager RaddollManager;

    private void Awake()
    {
        RaddollManager = GetComponent<RaddollManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        healthTimer = 2;
    }

    // Update is called once per frame
    private void Update()
    {
        healthTimer += Time.deltaTime;
        if (health <= 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Ground"))
        {
            //Debug.Log("hit simmin");
        }

        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Physics Object"))
        {
            Debug.Log("hit an enemy gamer");
            if (healthTimer > IFrameTime)
            {
                health--;
                healthTimer = 0;
                var dir = Vector3.Normalize(transform.position - collision.transform.position) * speed * 0.2f;
                
                var collisionPunchResponse = GetComponentInParent<PunchResponse>();
                collisionPunchResponse.DestroyAI();

                var collisionRagdoll = collisionPunchResponse.gameObject.GetComponent<RaddollManager>();
                collisionRagdoll.ragDollEnabled = true;
                collisionRagdoll.EnableRagdoll();
                
                foreach (var body in collisionRagdoll.Rigidbodies)
                {
                    body.AddForceAtPosition(dir, new Vector3(0, 0, 0));
                }
            }
        }

        if (collision.transform.CompareTag("Wisp"))
        {
            var xScale = collision.transform.localScale.x;

            var dir = gameObject.GetComponent<Rigidbody>().velocity;

            dir.Normalize();
            dir *= speed;
            gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-dir, new Vector3(0, 0, 0));
        }
    }

    public void Punched(Vector3 axis)
    {
        DestroyAI();
        direction = axis * speed;
        //GetComponent<Rigidbody>().AddForceAtPosition(direction, new Vector3(0, 0, 0));
        RaddollManager.ragDollEnabled = true;
        RaddollManager.EnableRagdoll();

        foreach (var body in RaddollManager.Rigidbodies)
        {
            body.AddForceAtPosition(direction, new Vector3(0, 0, 0));
        }
    }

    public void DestroyAI()
    {
        Destroy(navMeshAgent);
        Destroy(GetComponent<MQSAI>());
    }
}