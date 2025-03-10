using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedPopup : MonoBehaviour
{
    public void Btn_RePlay()
    {
        SoundManager.Instance.Play_SFX(SFX_SoundType.BUTTON, 0.1f);
        
        SceneManager.LoadScene("MainScene");
    }
}
