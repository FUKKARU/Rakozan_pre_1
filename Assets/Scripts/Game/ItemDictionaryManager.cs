using Ex;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDictionaryManager
{
    // Return true if the new item was added.
    public static bool Add(this List<(string, int)> items, string key)
    {
        bool isFound = items.Target(key, (e) => (e.Item1, e.Item2 + 1), (key) => items.Add((key, 1)));
        return !isFound;
    }

    // Return true if the existing item was removed.
    public static bool Sub(this List<(string, int)> items, string key)
    {
        bool isFound = items.Target(key, (e) => (e.Item1, e.Item2 - 1), (s) => Flow.Pass());
        items.Reflesh(key);
        return isFound;
    }

    static void Reflesh(this List<(string, int)> items, string key)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Item1 == key)
            {
                if (items[i].Item2 <= 0)
                {
                    items.Remove((items[i].Item1, items[i].Item2));
                }
            }
        }
    }
}
