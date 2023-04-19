using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Image starting;
    [SerializeField] private Image onePeice;
    [SerializeField] private Image finale;
    private void Start()
    {
        starting.enabled =false;
        onePeice.enabled =false;
        finale.enabled =false;

    }
    public void peiceCollected(int artifactNum)
    {

        switch (artifactNum)
        {
            case 3:
                starting.enabled =true;
                finale.enabled = false;
                break;
            case 2:
                onePeice.enabled = true;
                finale.enabled = false;
                break;
            case 1:
                finale.enabled = true;
                onePeice.enabled = false;
                starting.enabled = false;
                break;
            default:
                break;
        }

    }
}

