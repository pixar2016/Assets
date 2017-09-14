
using System.Collections.Generic;
using UnityEngine;

public class StaticEffectView : EffectView
{
    public StaticEffectInfo effectInfo;
    public float effectTime;
    public float effectMaxTime;
    public StaticEffectView(StaticEffectInfo _effectInfo)
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
        effectObj.transform.position = effectInfo.effPos;
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

