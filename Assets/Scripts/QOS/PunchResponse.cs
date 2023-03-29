using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchResponse : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] int health = 3;
    private float healthTimer;
    [SerializeField] float IFrameTime=2;
    [SerializeField]
    private RaddollManager RaddollManager;
    // Start is called before the first frame update
    void Start()
    {
        healthTimer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        healthTimer += Time.deltaTime;
        if (health<=0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }   
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Ground")
        {
            Debug.Log("hit simmin");
        }
        if (collision.transform.tag=="Enemy")
        {
            Debug.Log("hit an enemy gamer");
            if (healthTimer>IFrameTime)
            {
                health--;
                healthTimer = 0;
                Vector3 dir = Vector3.Normalize(transform.position - collision.transform.position)*speed*0.2f;

                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                GetComponent<Rigidbody>().AddForceAtPosition(dir, new Vector3(0, 0, 0));

                collision.transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                collision.transform.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-dir, new Vector3(0, 0, 0));
                
            }

        }
        if (collision.transform.tag == "Wisp")
        {
            float xScale = collision.transform.localScale.x;

            Vector3 dir = gameObject.GetComponent<Rigidbody>().velocity;

            dir.Normalize();
            dir *= speed;
            gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-dir, new Vector3(0, 0, 0));
            

        }
    }
    public void Punched(Vector3 axis)
    {
        direction = axis*speed;
        //GetComponent<Rigidbody>().AddForceAtPosition(direction, new Vector3(0, 0, 0));
        RaddollManager.ragDollEnabled=true;
        RaddollManager.EnableRagdoll();
        foreach (Rigidbody body in RaddollManager.Rigidbodies)
        {
            body.AddForceAtPosition(direction, new Vector3(0, 0, 0));
        }
    }
}
