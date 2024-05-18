using Ex;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RandomMaze
{
    public class GameManager : MonoBehaviour
    {
        #region staticかつシングルトンにする
        public static GameManager Instance { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        [System.NonSerialized] public bool IsClear = false;

        [SerializeField] GameObject gameUI;
        [SerializeField] GameObject clearUI;

        // 時間が繰り上がる境界（sec => min, com => min）
        readonly int[] timeConst = new int[] { 60, 100 };
        // 時間の表記を何桁揃えにするか
        const int digits = 2;

        // min,sec,com
        float time = 0;
        TextMeshProUGUI timer;
        bool isClearUIShowed = false;

        void Start()
        {
            timer = gameUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

            gameUI.SetActive(true);
            clearUI.SetActive(false);
        }

        void Update()
        {
            if (IsClear)
            {
                if (!isClearUIShowed)
                {
                    isClearUIShowed = true;

                    gameUI.SetActive(false);
                    clearUI.SetActive(true);
                    (int min, int sec, int thi) = time.NormalizedTime();
                    TextMeshProUGUI clearText = clearUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                    clearText.text = $"Clear\r\n<size=36>{min.ToString($"D{digits}")}:{sec.ToString($"D{digits}")}:{thi.ToString($"D{digits}")}</size>";
                }
                else if (KeyCode.R.Down())
                {
                    // リトライ
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            else
            {
                time += Time.deltaTime;
                (int min, int sec, int thi) = time.NormalizedTime();
                timer.text = $"{min.ToString($"D{digits}")}:{sec.ToString($"D{digits}")}:{thi.ToString($"D{digits}")}";
            }
        }
    }
}
