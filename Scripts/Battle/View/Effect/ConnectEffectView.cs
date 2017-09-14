
using System.Collections.Generic;
using UnityEngine;

public class ConnectEffectView : EffectView
{
    public ConnectEffectInfo effectInfo;
    public float effectTime;
    public float effectMaxTime;
    public ConnectEffectView(ConnectEffectInfo _effectInfo)
    {
        Id = _effectInfo.Id;
        effectInfo = _effectInfo;
        effectTime = 0;
        effectMaxTime = 1;
    }

    public override void InitCom()
    {
        InitAnimate(effectObj, effectInfo.effectName, effectInfo.GetWidth());
        InitPos();
    }

    private void InitPos()
    {
        Vector3 startPos = effectInfo.startPos;
        Vector3 endPos = effectInfo.endPos;
        Vector3 angle = Vector3.zero;
        angle.z = angle_360(Vector3.left, endPos - startPos);
        effectObj.transform.position = (startPos + endPos) / 2;
        effectObj.transform.eulerAngles = angle;
    }

    public override void Update()
    {
        effectTime += Time.deltaTime;
        if (effectTime >= effectMaxTime)
        {
            effectTime = 0;
            EntityManager.getInstance().RemoveEffect(effectInfo.Id);
        }
    }
}

