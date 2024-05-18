using Ex;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RandomMaze
{
    public class GameManager : MonoBehaviour
    {
        #region static���V���O���g���ɂ���
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

        // ���Ԃ̕\�L�����������ɂ��邩
        const int digits = 2;

        // min,sec,com
        float time = 0;
        TextMeshProUGUI timer;
        bool isClearUIShowed = false;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

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
                    // ���g���C
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
