using Ex;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomMaze
{
    public class PlayerLight : MonoBehaviour
    {
        [SerializeField] new GameObject light;

        void Update()
        {
            if (GameManager.Instance.IsClear) enabled = false;
            else if (KeyCode.Q.Down()) light.SetActive(!light.activeSelf);
        }
    }
}
