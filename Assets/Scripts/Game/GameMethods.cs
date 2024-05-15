using Ex;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMethods : MonoBehaviour
{
    [NonSerialized] public GameObject Escape;
    TextMeshProUGUI _Time;
    Button ShowMenu;
    public GameObject MenuPrfb;
    GameObject Menu;
    GameObject MenuParent;
    Button Title;
    Button TitleYes;
    Button TitleNo;
    GameObject Confirmation;
    [Space(25)]
    Button Config;
    Button Book;
    GameObject _Config;
    GameObject _Book;
    [Space(25)]
    List<GameObject> BookItems;
    GameObject Description;

    float time;

    void Start()
    {
        Escape = "EscapeUI_Game".FindTag();
        _Time = "TimeText_Game".FindTag<TextMeshProUGUI>();
        ShowMenu = "MenuButton_Game".FindTag<Button>();
        Title = "TitleButton_Game".FindTag<Button>();
        TitleYes = "TitleYesButton_Game".FindTag<Button>();
        TitleNo = "TitleNoButton_Game".FindTag<Button>();
        Confirmation = "ConfirmationUI".FindTag();

        Confirmation.SetActive(false);
        Escape.SetActive(false);

        MenuParent = "Canvas_Game".FindTag();
        Menu = Instantiate(MenuPrfb, MenuParent.transform);
        Config = "ConfigButton".FindTag<Button>();
        Book = "BookButton".FindTag<Button>();
        _Config = "ConfigUI".FindTag();
        _Book = "BookUI".FindTag();
        BookItems = "BookItem".FindsTag();
        Description = "DescriptionUI".FindTag();

        _Book.SetActive(false);
        Menu.SetActive(false);
        Description.SetActive(false);

        ShowMenu.OnClick(() => Methods.Game_ShowMenu(Menu));
        Config.OnClick(() => Methods.Menu_Config(_Config, _Book));
        Book.OnClick(() => Methods.Menu_Book(_Config, _Book));
        foreach (GameObject bookItem in BookItems)
        {
            bookItem.GetComponent<Button>().onClick.AddListener(() => Methods.Menu_Book_ShowDescription(Description, bookItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text));
        }

        Title.OnClick(() => Methods.Game_ShowTitleConfirmation(Confirmation));
        TitleYes.OnClick(() => Methods.Game_BackToTitle());
        TitleNo.OnClick(() => Methods.Game_HideTitleConfirmation(Confirmation));
    }

    void Update()
    {
        if (!Escape.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            (int min, int sec, int thi) = time.NormalizedTime();
            _Time.text = $"{min.ToString("D2")}:{sec.ToString("D2")}:{thi.ToString("D2")}";
            Methods.Game_ShowEscape(Escape);
        }

        if (Escape.activeSelf && !Menu.activeSelf && !Confirmation.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.Game_HideEscape(Escape);
            Time.timeScale = 1;
        }
        else if (Escape.activeSelf && Menu.activeSelf && !Description.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.Menu_Close(Menu);
        }
        else if (Escape.activeSelf && Menu.activeSelf && Description.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.Menu_Book_CloseDescription(Description);
        }

        time += Time.deltaTime;
    }
}