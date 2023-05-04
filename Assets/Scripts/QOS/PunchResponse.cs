 using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;
public class PunchResponse : MonoBehaviour
{
    public enum KnightType {smol, red, blue, green };

    [SerializeField] private float speed;
    [SerializeField] private int health = 3;
    [SerializeField] private float IFrameTime = 2;
    [SerializeField] KnightType type;
    private Vector3 direction;

    private float deathLeway;
    private float deathLeeWayTimer;

    [SerializeField] private DisolveManager[] disolveManagers;
    private bool punchable;

    private float healthTimer;
    private NavMeshAgent navMeshAgent;

    private RaddollManager RaddollManager;

    private void Awake()
    {
        disolveManagers = GetComponentsInChildren<DisolveManager>();
        RaddollManager = GetComponent<RaddollManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthTimer = 2;
        punchable = true;
        deathLeway = 3f;
        deathLeeWayTimer = 0;
    }

    // Start is called before the first frame update
    private void Start()
    {
 
    }

    // Update is called once per frame
    private void Update()
    {
        healthTimer += Time.deltaTime;
        if (RaddollManager.ragDollEnabled&&RaddollManager.gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude<0.02)
        {
            deathLeeWayTimer += Time.deltaTime;
            if (deathLeeWayTimer>deathLeway)
            {
                punchable = false;
                foreach (DisolveManager dis in disolveManagers)
                {
                    dis.disolveMult = -1;
                }
                Destroy(gameObject, disolveManagers[0].disolveDuration);
            }
            
        }


     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (punchable) { 
            if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Physics Object"))
            {
                if (collision.transform.CompareTag("Physics Object"))
                {
                    if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.25)
                    {
                        return;
                    }
                }
                Debug.Log("hit an enemy gamer");

                if (healthTimer > IFrameTime)
                {
                    if (collision.transform.CompareTag("Enemy")&&type==KnightType.green)
                    {
                        return;
                    }
                    health--;
                    healthTimer = 0;
                    if (health==0)
                    {
                        var dir = Vector3.Normalize(transform.position - collision.transform.position) * speed * 0.2f;

                        var collisionPunchResponse = GetComponentInParent<PunchResponse>();
                        collisionPunchResponse.DestroyAI();

                        var collisionRagdoll = collisionPunchResponse.gameObject.GetComponent<RaddollManager>();
                        Flying(dir, collisionRagdoll);
                    }
                    
                }
            }
        }
    }

    public void Punched(Vector3 axis)
    {
        if (type ==KnightType.green&&!RaddollManager.ragDollEnabled)
        {
            return;
        }
        if (punchable)
        {
            health--;
            health = Math.Max(health, 0);
            if (health == 0)
            {
                deathLeeWayTimer = 0;
                DestroyAI();
                direction = axis * speed;     
                Flying(direction, RaddollManager);
            }
            else
            {
                direction = axis * speed;
                StartCoroutine("PushBack", direction);
            }
        }

    }

    private void Flying(Vector3 dir, RaddollManager raddollManager)
    {
        raddollManager.ragDollEnabled = true;
        raddollManager.EnableRagdoll();

        foreach (var body in raddollManager.Rigidbodies)
        {
            body.AddForceAtPosition(direction, new Vector3(0, 0, 0));
        }
        GetComponent<Rigidbody>().isKinematic = true;

    }
    IEnumerator PushBack(Vector3 dir)
    {
        navMeshAgent.enabled = false;
        Rigidbody body = GetComponent<Rigidbody>();
        body.AddForceAtPosition(direction * 0.5f, new Vector3(0, 0, 0));
        body.constraints = RigidbodyConstraints.FreezePositionY;
        body.freezeRotation = true;
        yield return new WaitForSeconds(1);
        navMeshAgent.enabled = true;
        body.freezeRotation = false;
        body.constraints = RigidbodyConstraints.None;
    }

    private void DestroyAI()
    {
        Destroy(navMeshAgent);
        Destroy(GetComponent<MQSAI>());
    }

}