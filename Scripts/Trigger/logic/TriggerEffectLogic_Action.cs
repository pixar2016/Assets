using System;
using System.Collections.Generic;
using UnityEngine;
public class TriggerEffectLogic_Action : TriggerEffectBase
{
    public TriggerEffectLogic_Action()
    {

    }
    public override void ExcuteAction(TriggerInfo triggerInfo, TriggerEffectInfo effectInfo)
    {
        CharacterInfo charInfo = triggerInfo.charInfo;
        //Debug.Log("TriggerGroupId = "+ triggerInfo.triggerGroup.Id + "TriggerEffectLogic_Action");
        if (effectInfo.paramList == null || effectInfo.paramList[0] == null)
        {
            return;
        }
        charInfo.DoAction(effectInfo.paramList[0]);
    }
}

