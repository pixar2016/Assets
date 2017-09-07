
using System.Collections.Generic;
using UnityEngine;


public class TriggerEffectLogic_ConnectEffect : TriggerEffectBase
{
    public TriggerEffectLogic_ConnectEffect()
    {

    }

    public override void ExcuteAction(TriggerInfo triggerInfo, TriggerEffectInfo effectInfo)
    {
        if (effectInfo.paramList == null || effectInfo.paramList[0] == null)
        {
            return;
        }
        CharacterInfo charInfo = triggerInfo.charInfo;
        CharacterInfo targetInfo = triggerInfo.triggerGroup.triggerlogicData.targetInfo;
        int effectId = int.Parse(effectInfo.paramList[0]);
        EntityManager.getInstance().AddConnectEffect(effectId, charInfo, targetInfo);
    }
}

