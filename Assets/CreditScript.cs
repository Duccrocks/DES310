using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RectTransform scroller;

    public float creditTime;

    void Start()
    {
        //scroller.SetPositionAndRotation(new Vector3(0,-850,0), Quaternion.identity);
        Vector3 pos = scroller.localPosition;
        pos.y = -850;
        scroller.localPosition = pos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = scroller.localPosition;
        pos.y += (3850f/creditTime) * Time.deltaTime;
        scroller.localPosition = pos;


        if (pos.y>2000)
        {
            //GO TO TITLE
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //GO TO TITLE
        }
    }
}
