using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Image starting;
    [SerializeField] private Image onePiece;
    [SerializeField] private Image finale;
    private void Start()
    {
        starting.enabled = false;
        onePiece.enabled = false;
        finale.enabled = false;

    }
    public void PieceCollected(int artifactNum)
    {

        switch (artifactNum)
        {
            case 3:
                starting.enabled = true;
                finale.enabled = false;
                break;
            case 2:
                onePiece.enabled = true;
                finale.enabled = false;
                break;
            case 1:
                finale.enabled = true;
                onePiece.enabled = false;
                starting.enabled = false;
                break;
            default:
                break;
        }

    }
}

