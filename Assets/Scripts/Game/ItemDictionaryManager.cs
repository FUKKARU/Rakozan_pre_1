using Ex;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDictionaryManager
{
    public static bool Add(this List<(string, int)> items, string key)
    {
        bool isFound = items.Target(key, (e) => (e.Item1, e.Item2 + 1), (key) => items.Add((key, 1)));
        return isFound && items.Count <= 1;
    }

    // return true if the key was removed.
    public static bool Sub(this List<(string, int)> items, string key)
    {
        bool isFound = items.Target(key, (e) => (e.Item1, e.Item2 - 1), (s) => Flow.Pass());
        items.Reflesh(key);
        return isFound;
    }

    static void Reflesh(this List<(string, int)> items, string key)
    {
        items.Target(key, (e) => e.Item2 <= 0, (e) => items.Remove(e), (e) => Flow.Pass());
    }
}
