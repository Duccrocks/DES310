using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private RectTransform compassTrans;
    public float rotation;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
      
        compassTrans.rotation = Quaternion.Euler(0,0,player.rotation.eulerAngles.y-90);
    }
}
