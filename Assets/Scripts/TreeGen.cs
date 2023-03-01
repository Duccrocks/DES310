using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreeGen : MonoBehaviour
{
    [SerializeField] GameObject[] trees;
    [SerializeField] int treeCount;
    [SerializeField] GameObject treeParent;
    [SerializeField] LayerMask layerMask;

    public void Generate()
    {
        foreach (Transform child in treeParent.transform) Object.DestroyImmediate(child.gameObject);

        for (int i = 0; i < treeCount; i++)
        {
            Vector2 spawnPoint = new Vector2(Random.Range(-250, 100), Random.Range(250, -50));

            Physics.Raycast(new Vector3(spawnPoint.x, 50.0f, spawnPoint.y), Vector3.down, out var hit, 100.0f, layerMask);

            GameObject tree = Instantiate(trees[Random.Range(0, trees.Length - 1)], hit.point, Quaternion.identity, treeParent.transform);
            tree.transform.Rotate(new Vector3(-90, 0, 0));
        }
    }
}
