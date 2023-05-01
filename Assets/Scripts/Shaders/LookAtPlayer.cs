using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform Player;

    void Update()
    {
        // Calculate the vector pointing from the quad to the player
        Vector3 direction = Player.position - transform.position;
        direction.y = 0; // project onto the xz plane

        // Rotate the quad only around its y-axis to face the direction
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
