using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

//Static class for static functions and extension functions.
public static class Helpers
{
    /// <summary>
    ///     Takes a list and returns a random element from it.
    /// </summary>
    /// <returns>A random element from this list. </returns>
    public static T Rand<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    /// <summary>
    ///     Takes an array and returns a random element from it.
    /// </summary>
    /// <returns>A random element from this array. </returns>
    public static T Rand<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    /// <summary>
    ///     Destroys all of this transforms children.
    /// </summary>
    public static void DestroyChildren(this Transform transforms)
    {
        foreach (Transform child in transforms) Object.Destroy(child.gameObject);
    }

    /// <summary>
    ///     Destroys all of this transforms children.
    /// </summary>
    public static void DestroyChildrenImmeditately(this Transform transforms)
    {
        foreach (Transform child in transforms) Object.DestroyImmediate(child.gameObject);
    }

    /// <summary>
    ///     Timer that counts down.
    /// </summary>
    /// <param name="time">Time in seconds to count down from.</param>
    /// <returns>When finished counting.</returns>
    public static IEnumerator Timer(int time)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
    }
}