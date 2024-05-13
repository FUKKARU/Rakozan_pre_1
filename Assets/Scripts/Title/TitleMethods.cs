using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ex;

public class TitleMethods : MonoBehaviour
{
    string selectedStage;

    public Button GameStart;
    public GameObject StageSelect;
    public Button ShowMenu;
    public GameObject MenuPrfb;
    GameObject Menu;
    public GameObject MenuParent;
    public Button Quit;
    [Space(25)]
    public Button Tutorial;
    public Button GameStage;
    public GameObject _Difficulty;
    public Button Easy;
    public Button Normal;
    public Button Hard;
    [Space(25)]
    Button Config;
    Button Book;
    GameObject _Config;
    GameObject _Book;
    [Space(25)]
    List<GameObject> BookItems;
    GameObject Description;

    void Start()
    {
        GameStart.OnClick(() => Methods.Title_GameStart(StageSelect));

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

        ShowMenu.OnClick(() => Methods.Title_ShowMenu(Menu));
        Config.OnClick(() => Methods.Menu_Config(_Config, _Book));
        Book.OnClick(() => Methods.Menu_Book(_Config, _Book));
        foreach (GameObject bookItem in BookItems)
        {
            bookItem.GetComponent<Button>().onClick.AddListener(() => Methods.Menu_Book_ShowDescription(Description, bookItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text));
        }

        Quit.OnClick(() => Methods.Title_Quit());

        Tutorial.OnClick(() => { Methods.StageSelect_StageSelect(_Difficulty); selectedStage = "Tutorial"; });
        GameStage.OnClick(() => { Methods.StageSelect_StageSelect(_Difficulty); selectedStage = "Game"; });
        Easy.OnClick(() => Methods.StageSelect_DifficultySelect(selectedStage, 0));
        Normal.OnClick(() => Methods.StageSelect_DifficultySelect(selectedStage, 1));
        Hard.OnClick(() => Methods.StageSelect_DifficultySelect(selectedStage, 2));
    }

    void Update()
    {
        if (StageSelect.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.StageSelect_Close(StageSelect, _Difficulty);
        }
        else if (Menu.activeSelf && !Description.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.Menu_Close(Menu);
        }
        else if (Menu.activeSelf && Description.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.Menu_Book_CloseDescription(Description);
        }
    }
}
