using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleMethods : MonoBehaviour
{
    string selectedStage;

    public Button GameStart;
    public GameObject StageSelect;
    public Button ShowMenu;
    public GameObject Menu;
    public Button Quit;
    [Space(25)]
    public Button Tutorial;
    public Button GameStage;
    public GameObject _Difficulty;
    public Button Easy;
    public Button Normal;
    public Button Hard;
    [Space(25)]
    public Button Config;
    public Button Book;
    public GameObject _Config;
    public GameObject _Book;
    [Space(25)]
    public Button[] BookItems;
    public GameObject Description;

    void Start()
    {
        GameStart.onClick.AddListener(() => Methods.Title_GameStart(StageSelect));
        ShowMenu.onClick.AddListener(() => Methods.Title_ShowMenu(Menu));
        Quit.onClick.AddListener(() => Methods.Title_Quit());

        Tutorial.onClick.AddListener(() => { Methods.StageSelect_StageSelect(_Difficulty); selectedStage = "Tutorial"; });
        GameStage.onClick.AddListener(() => { Methods.StageSelect_StageSelect(_Difficulty); selectedStage = "Game"; });
        Easy.onClick.AddListener(() => Methods.StageSelect_DifficultySelect(0));
        Normal.onClick.AddListener(() => Methods.StageSelect_DifficultySelect(1));
        Hard.onClick.AddListener(() => Methods.StageSelect_DifficultySelect(2));

        Config.onClick.AddListener(() => Methods.Menu_Config(_Config, _Book));
        Book.onClick.AddListener(() => Methods.Menu_Book(_Config, _Book));

        foreach (Button bookItem in BookItems)
        {
            bookItem.onClick.AddListener(() => Methods.Menu_Book_ShowDescription(Description, bookItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text));
        }
    }

    void Update()
    {
        if (StageSelect.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.StageSelect_Close(StageSelect);
        }

        if (Menu.activeSelf && !Description.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.Menu_Close(Menu);
        }

        if (Menu.activeSelf && Description.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.Menu_Book_CloseDescription(Description);
        }
    }
}
