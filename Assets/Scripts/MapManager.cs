using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Image starting;
    [SerializeField] private Image onePiece;
    [SerializeField] private Image finale;
    [SerializeField] private Image panel;
    [SerializeField] private GameObject hud;
    bool mapOpened=false;
    private int artifactNum = 0;
    private void Start()
    {
        starting.enabled = false;
        onePiece.enabled = false;
        finale.enabled = false;
        panel.enabled = false;

    }
    public void PieceCollected(int artNum)
    {
        artifactNum = artNum;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapOpened)
            {
                CloseMap();
                mapOpened= false;
            }
            else
            {
                OpenMap();
                mapOpened = true;
            }
        }
    }
    public void OpenMap()
    {
        hud.SetActive(false);
        panel.enabled= true;
        switch (artifactNum)
        {
            case 3:
                starting.enabled = true;
                onePiece.enabled = false;
                finale.enabled = false;
                break;
            case 2:
                onePiece.enabled = true;
                starting.enabled = true;
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

    public void CloseMap()
    {
        hud.SetActive(true);
        panel.enabled= false;
        onePiece.enabled=false;
        finale.enabled=false;
        starting.enabled = false;
    }
}

