using System;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEffectLogic_Bullet : TriggerEffectBase
{
    public TriggerEffectLogic_Bullet()
    {

    }
    public override void ExcuteAction(TriggerInfo triggerInfo, TriggerEffectInfo effectInfo)
    {
        //Debug.Log("TriggerEffectLogic_Bullet");
        if (effectInfo.paramList == null || effectInfo.paramList[0] == null)
        {
            return;
        }
        CharacterInfo charInfo = triggerInfo.charInfo;
        CharacterInfo targetInfo = charInfo.GetTargetInfo();
        int effectId = int.Parse(effectInfo.paramList[0]);
        int pathType = int.Parse(effectInfo.paramList[1]);
        float speed = 160;//float.Parse(effectInfo.paramList[2]);
        //EntityManager.getInstance().AddBullet(1, charInfo, targetInfo, 200f, triggerInfo.triggerGroup.Id);
        EntityManager.getInstance().AddMoveEffect(effectId, charInfo, targetInfo, speed, pathType, triggerInfo.triggerGroup.Id);
    }
}

