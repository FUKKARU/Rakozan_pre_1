using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ex;
using TMPro;

public class ItemSelect : MonoBehaviour
{
    GameMethods GameMethods;

    public GameObject[] ItemFrames; // left is 0.
    public Sprite[] ItemImages; // 0 is "1", 4 is "5".
    int selectingIndex = 0; // right is 0

    Dictionary<string, int> items = new Dictionary<string, int>() // 1st => 5th
    {
        {"1", 1 },
        {"2", 0 },
        {"3", 1 },
        {"4", 3 },
        {"5", 2 },
    };

    void Start()
    {
        GameMethods = "GameMethods".FindTag<GameMethods>();
    }

    void Update()
    {
        Dictionary<string, int> existingItems = new(); // 1st => 5th
        foreach (int i in Collection.Range(5))
        {
            if (items[(i + 1).ToString()] != 0)
            {
                existingItems[(i + 1).ToString()] = items[(i + 1).ToString()];
            }
        }
        foreach (GameObject itemFrame in ItemFrames)
        {
            itemFrame.SetActive(true);
        }
        foreach (int i in Collection.Range(items.Count - existingItems.Count))
        {
            ItemFrames[i].SetActive(false);
        }

        // right is 1st, left is 5th.
        int idx = 4;
        foreach (var e in existingItems)
        {
            ItemFrames[idx].transform.GetChild(0).GetComponent<Image>().sprite = ItemImages[int.Parse(e.Key) - 1];
            ItemFrames[idx].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"*{e.Value}";
            idx--;
        }

        selectingIndex = Mathf.Clamp(selectingIndex, 0, existingItems.Count - 1);
        float mouseWheelInput = Input.GetAxisRaw("Mouse ScrollWheel");
        if (mouseWheelInput > 0.05f)
        {
            if (++selectingIndex >= existingItems.Count)
            {
                selectingIndex = 0;
            }
        }
        else if (mouseWheelInput < -0.05f)
        {
            if (--selectingIndex <= -1)
            {
                selectingIndex = existingItems.Count - 1;
            }
        }
        selectingIndex = Mathf.Clamp(selectingIndex, 0, existingItems.Count - 1);
        foreach (int i in Collection.Range(existingItems.Count))
        {
            Color col = ItemFrames[-(i + 1) + items.Count].GetComponent<Image>().color;
            col.a = i == selectingIndex ? 1 : 100 / 255f;
            ItemFrames[-(i + 1) + items.Count].GetComponent<Image>().color = col;
        }



        if (!GameMethods.Escape.activeSelf && Input.GetMouseButtonDown(1))
        {
            int usingItemNum = -1;
            foreach (GameObject itemFrame in ItemFrames)
            {
                if (itemFrame.GetComponent<Image>().color.a >= 0.9f)
                {
                    usingItemNum = Array.IndexOf(ItemImages, itemFrame.transform.GetChild(0).GetComponent<Image>().sprite) + 1;
                    usingItemNum.Show();
                }
            }
            
            items[usingItemNum.ToString()] = Mathf.Clamp(items[usingItemNum.ToString()] - 1, 0, 99);
        }
    }
}