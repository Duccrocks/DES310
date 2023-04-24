using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Texture red;
    [SerializeField] private Texture blue;
    [SerializeField] private Texture green;

    public void Colour()
    {
        BookColour[] booksArray = FindObjectsOfType<BookColour>();

        foreach (BookColour book in booksArray)
        {
            int rand = Random.Range(0, 2);
            switch (rand) {
                case 0: book.UsingTexture = green;
                    break;
                case 1: book.UsingTexture = blue;
                    break;
                case 2: book.UsingTexture = red;
                    break;
                default:
                    break;
            }
        }
    }
}
