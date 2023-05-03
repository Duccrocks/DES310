using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [Header("Map Images")] 
    [SerializeField] private Image bottomLeftImage;
    [SerializeField] private Image bottomRightImage;
    [SerializeField] private Image topLeftImage;
    [SerializeField] private Image topRightImage;
    
    [Header("HUD")]
    [SerializeField] private Image panel; 
    [SerializeField] private GameObject hudPanel;

    public bool bottomLeftCollected;
    private bool bottomRightCollected;
    private bool topLeftCollected;
    private bool topRightCollected;

    private bool mapOpened;

    private int artifactNum;
    private void Start()
    {
        bottomLeftCollected = false;
        bottomRightCollected = false;
        topLeftCollected = false;
        topRightCollected = false;
        CloseMap();
    }

    public void PieceCollected(int id)
    {
        switch (id)
        {
            case 0:
                topLeftCollected = true;
                break;
            case 1:
                topRightCollected = true;
                break;
            case 2:
                bottomRightCollected = true;
                break;
        }
    }

    public void ToggleMap()
    {
        if (!(bottomLeftCollected || bottomRightCollected || topLeftCollected || topRightCollected)) return;
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
        hudPanel.SetActive(false);
        panel.enabled = true;

        topLeftImage.enabled = topLeftCollected;
        topRightImage.enabled = topRightCollected;
        bottomLeftImage.enabled = bottomLeftCollected;
        bottomRightImage.enabled = bottomRightCollected;
    }

    private void CloseMap()
    {
        hudPanel.SetActive(true);

        panel.enabled = false;
        topLeftImage.enabled = false;
        topRightImage.enabled = false;
        bottomLeftImage.enabled = false;
        bottomRightImage.enabled = false;
    }
}