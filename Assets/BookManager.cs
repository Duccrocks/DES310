using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Texture red;
    [SerializeField] private Texture blue;
    [SerializeField] private Texture green;

    [SerializeField] private Material redMat;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material GreenMat;

    public void Colour()
    {
        BookColour[] booksArray = FindObjectsOfType<BookColour>();

        foreach (BookColour book in booksArray)
        {
            int rand = Random.Range(0, 3);
            switch (rand) {
                case 0:
                    book.gameObject.GetComponent<Renderer>().materials[0] = GreenMat;
                    break;
                case 1:
                    book.gameObject.GetComponent<Renderer>().materials[0] = blueMaterial;
                    break;
                case 2:
                    book.gameObject.GetComponent<Renderer>().materials[0] = redMat;
                    break;
                default:
                    break;
            }
        }
    }
}
