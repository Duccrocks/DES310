using UnityEditor;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    [SerializeField] Material[] materials;
    public void Colour()
    {
        BookColour[] booksArray = FindObjectsOfType<BookColour>();

        foreach (BookColour book in booksArray)
        {
            int rand = Random.Range(0, materials.Length);
            book.gameObject.GetComponent<Renderer>().material = materials[rand];
        }
    }
}
