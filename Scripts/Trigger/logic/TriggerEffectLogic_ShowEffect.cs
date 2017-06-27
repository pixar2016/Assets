using System;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEffectLogic_ShowEffect : TriggerEffectBase
{
    public TriggerEffectLogic_ShowEffect()
    {

    }

    public override void ExcuteAction(TriggerInfo triggerInfo, TriggerEffectInfo effectInfo)
    {
        Debug.Log("ShowEffect");
        if (effectInfo.paramList == null || effectInfo.paramList[0] == null)
        {
            return;
        }
        int effectId = int.Parse(effectInfo.paramList[0]);
        int showType = int.Parse(effectInfo.paramList[1]);
        if (showType == 1)
        {
            Vector3 pos = triggerInfo.charInfo.GetPosition();
            EntityManager.getInstance().AddStaticEffect(effectId, pos);
        }
        else if (showType == 2)
        {
            Vector3 pos = triggerInfo.triggerGroup.triggerlogicData.pos;
            EntityManager.getInstance().AddStaticEffect(effectId, pos);
        }
    }
}

