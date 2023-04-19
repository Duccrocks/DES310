using UnityEngine;

public class GenericPunchResponse : MonoBehaviour
{

    [SerializeField] private float speed = 3000;
    [SerializeField] private int health = 3;
    [SerializeField] private float IFrameTime = 2;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void Punched(Vector3 axis)
    {
       Vector3 direction = axis * speed;
        rb.AddForce(direction);
    }

}
