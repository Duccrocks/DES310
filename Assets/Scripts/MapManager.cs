using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Image BottomLeftImage;
    [SerializeField] private Image BottomRightImage;
    [SerializeField] private Image TopLeftImage;
    [SerializeField] private Image TopRightImage;
    [SerializeField] private Image panel;
    [SerializeField] private GameObject hud;
    private int artifactNum;

    private bool mapOpened;

    public bool bottomRightCollected;
    public bool bottomLeftCollected;
    public bool TopRightCollected;
    public bool TopLeftCollected;

    private void Start()
    {
        bottomLeftCollected= false;
        bottomRightCollected= false;
        TopLeftCollected= false;
        TopRightCollected= false;
        CloseMap();
        
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

    public void PieceCollected(int id)
    {
        switch (id)
        {
            case 0: TopLeftCollected = true;
                break;
            case 1: TopRightCollected= true;
                break;
            case 2: bottomRightCollected= true;
                break;
            default: break;
        }
    }

    public void ToggleMap()
    {
        
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

        TopLeftImage.enabled = TopLeftCollected;
        TopRightImage.enabled = TopRightCollected;
        BottomLeftImage.enabled = bottomLeftCollected;
        BottomRightImage.enabled = bottomRightCollected;
    }

    private void CloseMap()
    {
        hud.SetActive(true);

        panel.enabled = false;
        TopLeftImage.enabled = false;
        TopRightImage.enabled = false;
        BottomLeftImage.enabled = false;
        BottomRightImage.enabled = false;
    }
}