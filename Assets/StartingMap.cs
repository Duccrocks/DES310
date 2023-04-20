using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingMap : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        try
        {
            RadarGameManager.Instance.collectStartingMap();
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogError(e);
        }
        Destroy(gameObject);
    }
}
