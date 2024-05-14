using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ex;
using TMPro;

public class ItemSelect : MonoBehaviour
{
    // Right is index 0.

    GameMethods GameMethods;

    public List<GameObject> ItemFrames;
    public List<Sprite> ItemImages; // o is "1", 4 is "5".
    int selectingIndex = 0;
    int SelectingIndex
    {
        get
        {
            return selectingIndex;
        }
        set
        {
            selectingIndex = value.Clamp(0, ItemFrames.Count - 1);
        }
    }

    float mouseWheelInput = 0;
    const float itemShowTime = 2;
    float itemShowingTime = 0;
    bool isShowingUI = false;

    List<(string, int)> items = new()
    {
        ("1", 1),
        ("4", 3),
        ("5", 2),
        ("3", 1)
    };

    void Start()
    {
        GameMethods = "GameMethods".FindTag<GameMethods>();
    }

    void Update()
    {
        UpdateItemUI();

        // Get mouse wheel input, and judge if UI should be shown.
        mouseWheelInput = Input.GetAxisRaw("Mouse ScrollWheel");
        if (Mathf.Abs(mouseWheelInput) < 0.01f)
        {
            itemShowingTime += Time.deltaTime;
            if (itemShowingTime >= itemShowTime)
            {
                itemShowingTime = 0;

                isShowingUI = false;
            }
        }
        else
        {
            itemShowingTime = 0;

            isShowingUI = true;
        }

        // Add/Sub an item.
        if (!GameMethods.Escape.activeSelf)
        {
            if (Input.GetMouseButtonDown(1) && items.Count > 0)
            {
                SelectingIndex.Clamp(0, ItemFrames.Count - 1);

                int usingItemNum = ItemImages.IndexOf(ItemFrames[SelectingIndex].GetChildComponent<Image>(0).sprite) + 1;

                bool isKeyDeleted = items.Sub(usingItemNum.ToString());

                if (isKeyDeleted)
                {
                    while (SelectingIndex > items.Count - 1 && items.Count > 0) SelectingIndex--;
                }

                UpdateItemUI();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1)) { items.Add("1"); UpdateItemUI(); }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { items.Add("2"); UpdateItemUI(); }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { items.Add("3"); UpdateItemUI(); }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) { items.Add("4"); UpdateItemUI(); }
            else if (Input.GetKeyDown(KeyCode.Alpha5)) { items.Add("5"); UpdateItemUI(); }
        }
    }

    void UpdateItemUI()
    {
        if (isShowingUI)
        {
            DisplayItems();
        }
        else
        {
            if (items.Count > 0)
            {
                int usingItemNum = ItemImages.IndexOf(ItemFrames[SelectingIndex].GetChildComponent<Image>(0).sprite) + 1;
                (string, int) selectingItem = items.Find(usingItemNum.ToString());
                items.Remove(selectingItem);
                items.Insert(0, selectingItem);

                selectingIndex = 0;

                DisplayItems();

                Collection.Map(ItemFrames, (e) => e.SetActive(false));
                ItemFrames[0].SetActive(true);
            }
        }
    }

    void DisplayItems()
    {
        // Only show frames for existing item.
        Collection.Map(ItemFrames, (e) => e.SetActive(true));
        Collection.Map(
            Collection.Range(ItemFrames.Count - items.Count),
            (e) => ItemFrames[-(e + 1) + ItemFrames.Count].SetActive(false));

        // Set sprite and text.
        foreach ((int i, (string key, int value)) in Collection.Enumerate(items))
        {
            ItemFrames[i].GetChildComponent<Image>(0).sprite = ItemImages[int.Parse(key) - 1];
            ItemFrames[i].GetChildComponent<TextMeshProUGUI>(1).text = $"*{value}";
        }

        // Change the selecting item.
        if (mouseWheelInput > 0.05f) SelectingIndex = Math.Scroll(items.Count, SelectingIndex, 1);
        else if (mouseWheelInput < -0.05f) SelectingIndex = Math.Scroll(items.Count, SelectingIndex, -1);

        // Paint the selecting frame with black.
        foreach (int i in Collection.Range(items.Count))
        {
            Color col = ItemFrames[i].GetComponent<Image>().color;
            col.a = i == SelectingIndex ? 1 : 100 / 255f;
            ItemFrames[i].GetComponent<Image>().color = col;
        }
    }
}