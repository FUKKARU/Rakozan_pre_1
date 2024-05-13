using Ex;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GameMethods : MonoBehaviour
{
    GameObject Escape;
    TextMeshProUGUI _Time;
    Button Menu;
    Button Title;

    float time;

    void Start()
    {
        Escape = "EscapeUI_Game".FindTag();
        _Time = "TimeText_Game".FindTag().GetComponent<TextMeshProUGUI>();
        Menu = "MenuButton_Game".FindTag().GetComponent<Button>();
        Title = "TitleButton_Game".FindTag().GetComponent<Button>();

        Escape.SetActive(false);
    }

    void Update()
    {
        if (!Escape.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            int min = (int)(time / 60);
            float time_ = time - min * 60;
            int sec = (int)time_;
            int thi = (int)((time_ - sec) * 100);
            _Time.text = $"{min.ToString("D2")}:{sec.ToString("D2")}:{thi.ToString("D2")}";
            Methods.Game_ShowEscape(Escape);
        }
        else if (Escape.activeSelf && Input.GetMouseButtonDown(1))
        {
            Methods.Game_HideEscape(Escape);
            Time.timeScale = 1;
        }

        time += Time.deltaTime;
    }
}