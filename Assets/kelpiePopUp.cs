using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kelpiePopUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject popUp;
    [SerializeField] GameObject hud;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            show();
        }
        if(Input.GetKey(KeyCode.Escape)) { 
            hide(); 
        }
    }

    public void show()
    {
        popUp.active = true;
        hud.active = false;
    }

    public void hide()
    {
        popUp.active = false;
        hud.active = true;
    }
}
