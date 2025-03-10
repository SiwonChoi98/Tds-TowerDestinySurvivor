using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedPopup : MonoBehaviour
{
    public void Btn_RePlay()
    {
        SceneManager.LoadScene("MainScene");
    }
}
