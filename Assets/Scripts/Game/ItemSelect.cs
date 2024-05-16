using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ex;
using TMPro;
using System;

public class ItemSelect : MonoBehaviour
{
    // Right is 0th.

    GameMethods GameMethods;

    public List<GameObject> ItemFrames;
    public List<Sprite> ItemImages; // o is "1", 4 is "5".
    public List<GameObject> HotaruNums; // <Left is 0th!>
    int selectingIndex = 0;
    int SelectingIndex
    {
        get { return selectingIndex; }
        set { selectingIndex = value.Clamp(0, ItemFrames.Count - 1); }
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
        #region Get mouse wheel input, chage selection index, and judge if UI should be shown.
        mouseWheelInput = UnityEngine.Input.GetAxisRaw("Mouse ScrollWheel");

        if (items.Count > 0)
        {
            if (mouseWheelInput > 0.05f) SelectingIndex = Ex.Math.Scroll(items.Count, SelectingIndex, 1);
            else if (mouseWheelInput < -0.05f) SelectingIndex = Ex.Math.Scroll(items.Count, SelectingIndex, -1);
        }

        if (!isShowingUI && mouseWheelInput.Abs() > 0.01f) // Being wheeled.
        {
            itemShowingTime = 0;

            isShowingUI = true;
        }
        else if (isShowingUI && mouseWheelInput.Abs() < 0.01f) // Not being wheeled.
        {
            itemShowingTime += Time.deltaTime;

            if (itemShowingTime >= itemShowTime)
            {
                itemShowingTime = 0;

                if (items.Count > 0)
                {
                    // A number drawn on the image.
                    int usingItemNum = ItemImages.IndexOf(ItemFrames[SelectingIndex].GetChildComponent<Image>(0).sprite) + 1;

                    // Move the selecting item to the 1st.
                    (string, int) selectingItem = items.Find(usingItemNum.ToString());
                    items.Remove(selectingItem);
                    items.Insert(0, selectingItem);
                    selectingIndex = 0;
                }

                isShowingUI = false;
            }
        }
        else
        {
            itemShowingTime = 0;
        }
        #endregion

        #region Add/Sub an item.
        if (!GameMethods.Escape.activeSelf)
        {
            if (1.MouseDown() && items.Count > 0) // Sub.
            {
                int savedItemNum = items.Count;

                // A number drawn on the image.
                int usingItemNum = ItemImages.IndexOf(ItemFrames[SelectingIndex].GetChildComponent<Image>(0).sprite) + 1;

                // Sub.
                bool isExistingItemRemoved = items.Sub(usingItemNum.ToString());

                // The item was consumed up.
                if (items.Count > 0)
                {
                    if (isExistingItemRemoved && items.Count < savedItemNum)
                    {
                        while (SelectingIndex > items.Count - 1) SelectingIndex--;
                    }
                }
            }
            else if (KeyCode.Alpha1.Down()) items.Add("1"); // Add.
            else if (KeyCode.Alpha2.Down()) items.Add("2"); // Add.
            else if (KeyCode.Alpha3.Down()) items.Add("3"); // Add.
            else if (KeyCode.Alpha4.Down()) items.Add("4"); // Add.
            else if (KeyCode.Alpha5.Down()) items.Add("5"); // Add.
        }
        #endregion

        UpdateItemUI(items, ItemFrames, ItemImages, SelectingIndex, isShowingUI);
    }

    void UpdateItemUI(List<(string, int)> itemList, List<GameObject> frameList, List<Sprite> itemImages, int index, bool isShow)
    {
        if (itemList.Count > 0 && index >= itemList.Count) throw new ArgumentOutOfRangeException(nameof(itemList.Count));

        // Only show frames for existing item.
        Collection.Map(frameList, (e) => e.SetActive(true));
        Collection.Map(
            Collection.Range(frameList.Count - itemList.Count),
            (i) => frameList[-(i + 1) + frameList.Count].SetActive(false));

        // Set sprite and text.
        foreach ((int i, (string key, int value)) in Collection.Enumerate(itemList))
        {
            frameList[i].GetChildComponent<Image>(0).sprite = itemImages[int.Parse(key) - 1];
            frameList[i].GetChildComponent<TextMeshProUGUI>(1).text = $"*{value}";
        }

        // Paint the selecting frame with black.
        foreach ((int i, GameObject e) in Collection.Enumerate(frameList))
        {
            Color col = e.GetComponent<Image>().color;
            col.a = i == index ? 1 : 100 / 255f;
            e.GetComponent<Image>().color = col;
        }

        // Show Hotaru amount.
        Collection.Map(Collection.Enumerate(HotaruNums), (e) => e.value.GetComponent<Image>().enabled = e.index < GameData.HotaruNum);

        if (!isShow)
        {
            // Hide item UI.
            Collection.Map(frameList, (e) => e.SetActive(false));
            if (itemList.Count > 0) frameList[0].SetActive(true);

            // Hide Hotaru amount.
            Collection.Map(HotaruNums, (e) => e.GetComponent<Image>().enabled = false);
        }
    }
}