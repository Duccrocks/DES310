using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kelpiePopUp : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    [SerializeField] GameObject popUp;
    [SerializeField] GameObject hud;

    private void Update()
    {

        if (Input.GetKey(KeyCode.X))
        {
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

    public void Interact()
    {
        try
        {
            show();
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogError(e);
        }
       
    }
}
