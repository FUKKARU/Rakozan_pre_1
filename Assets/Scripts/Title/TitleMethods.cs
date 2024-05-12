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
    GameObject Menu = null;
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
    Button Config = null;
    Button Book = null;
    GameObject _Config = null;
    GameObject _Book = null;
    [Space(25)]
    GameObject[] BookItems = null;
    GameObject Description = null;

    void Start()
    {
        GameStart.onClick.AddListener(() => Methods.Title_GameStart(StageSelect));
        if (Menu == null)
        {
            Menu = Instantiate(MenuPrfb, MenuParent.transform);
            Config = "ConfigButton".FindTag().GetComponent<Button>();
            Book = "BookButton".FindTag().GetComponent<Button>();
            _Config = "ConfigUI".FindTag();
            _Book = "BookUI".FindTag();
            BookItems = "BookItem".FindsTag();
            Description = "DescriptionUI".FindTag();

            _Book.SetActive(false);
            Menu.SetActive(false);
            Description.SetActive(false);

            ShowMenu.onClick.AddListener(() => Methods.Title_ShowMenu(Menu));
            Config.onClick.AddListener(() => Methods.Menu_Config(_Config, _Book));
            Book.onClick.AddListener(() => Methods.Menu_Book(_Config, _Book));
            foreach (GameObject bookItem in BookItems)
            {
                bookItem.GetComponent<Button>().onClick.AddListener(() => Methods.Menu_Book_ShowDescription(Description, bookItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text));
            }
        }
        Quit.onClick.AddListener(() => Methods.Title_Quit());

        Tutorial.onClick.AddListener(() => { Methods.StageSelect_StageSelect(_Difficulty); selectedStage = "Tutorial"; });
        GameStage.onClick.AddListener(() => { Methods.StageSelect_StageSelect(_Difficulty); selectedStage = "Game"; });
        Easy.onClick.AddListener(() => Methods.StageSelect_DifficultySelect(selectedStage, 0));
        Normal.onClick.AddListener(() => Methods.StageSelect_DifficultySelect(selectedStage, 1));
        Hard.onClick.AddListener(() => Methods.StageSelect_DifficultySelect(selectedStage, 2));
    }

    void Update()
    {
        if (StageSelect.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.StageSelect_Close(StageSelect);
        }

        if (Menu != null && Description != null)
        {
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
}
