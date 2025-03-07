using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameResourceManager : Singleton<InGameResourceManager>
{
    private EtcData _etcData;

    protected override void Awake()
    {
        base.Awake();
        
        SetEtcData();
    }

    private void SetEtcData()
    {
        _etcData = Resources.Load<EtcData>("ETCData");
    }

    public EtcData GetEtcData()
    {
        return _etcData;
    }
}
