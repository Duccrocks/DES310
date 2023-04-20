using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Image starting;
    [SerializeField] private Image onePiece;
    [SerializeField] private Image finale;
    [SerializeField] private Image panel;
    [SerializeField] private GameObject hud;
    private int artifactNum;
    private bool mapOpened;

    private void Start()
    {
        starting.enabled = false;
        onePiece.enabled = false;
        finale.enabled = false;
        panel.enabled = false;
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.M))
    //     {
    //         if (mapOpened)
    //         {
    //             CloseMap();
    //             mapOpened = false;
    //         }
    //         else
    //         {
    //             OpenMap();
    //             mapOpened = true;
    //         }
    //     }
    // }

    public void PieceCollected(int artNum)
    {
        artifactNum = artNum;
    }

    public void ToggleMap()
    {
        if (artifactNum <= 0) return;
        if (mapOpened)
        {
            CloseMap();
            mapOpened = false;
        }
        else
        {
            OpenMap();
            mapOpened = true;
        }
    }

    private void OpenMap()
    {
        hud.SetActive(false);
        panel.enabled = true;
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
        }
    }

    private void CloseMap()
    {
        hud.SetActive(true);
        panel.enabled = false;
        onePiece.enabled = false;
        finale.enabled = false;
        starting.enabled = false;
    }
}