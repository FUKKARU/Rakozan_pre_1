using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerMove : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime);
    }
}