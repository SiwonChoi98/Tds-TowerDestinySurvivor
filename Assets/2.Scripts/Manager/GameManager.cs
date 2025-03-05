using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region UnityLifeSycle

    protected override void Awake()
    {
        base.Awake();

        InitTargetFrameRate(ProjectSettings.TargetFrameRate);
    }

    #endregion
    #region Settings

    public void InitTargetFrameRate(int frameRate)
    {
        Application.targetFrameRate = frameRate;
    }
    

    #endregion
}
