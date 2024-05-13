using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Methods
{
    public static void Title_GameStart(GameObject stageSelectUI)
    {
        stageSelectUI.SetActive(true);
    }

    public static void Title_ShowMenu(GameObject menuUI)
    {
        menuUI.SetActive(true);
    }

    public static void Title_Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public static void StageSelect_Close(GameObject stageSelectUI, GameObject difficultyUI)
    {
        difficultyUI.SetActive(false);
        stageSelectUI.SetActive(false);
    }

    public static void StageSelect_StageSelect(GameObject difficultyUI)
    {
        difficultyUI.SetActive(true);
    }

    public static void StageSelect_DifficultySelect(string sceneName, int dif)
    {
        Difficulty.Dif = dif;
        SceneManager.LoadScene(sceneName);
    }

    public static void Menu_Close(GameObject menuUI)
    {
        menuUI.SetActive(false);
    }

    public static void Menu_Config(GameObject configUI, GameObject bookUI)
    {
        configUI.SetActive(true);
        bookUI.SetActive(false);
    }

    public static void Menu_Book(GameObject configUI, GameObject bookUI)
    {
        configUI.SetActive(false);
        bookUI.SetActive(true);
    }

    public static void Menu_Book_ShowDescription(GameObject descriptionUI, string name)
    {
        descriptionUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
        descriptionUI.SetActive(true);
    }

    public static void Menu_Book_CloseDescription(GameObject descriptionUI)
    {
        descriptionUI.SetActive(false);
    }

    public static void Game_ShowEscape(GameObject escapeUI)
    {
        escapeUI.SetActive(true);
    }

    public static void Game_HideEscape(GameObject escapeUI)
    {
        escapeUI.SetActive(false);
    }

    public static void Game_ShowMenu(GameObject menuUI)
    {
        menuUI.SetActive(true);
    }

    public static void Game_ShowTitleConfirmation(GameObject confirmationUI)
    {
        confirmationUI.SetActive(true);
    }

    public static void Game_BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public static void Game_HideTitleConfirmation(GameObject confirmationUI)
    {
        confirmationUI.SetActive(false);
    }
}
