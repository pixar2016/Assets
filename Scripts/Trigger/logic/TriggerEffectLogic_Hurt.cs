using System;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEffectLogic_Hurt : TriggerEffectBase
{
    public TriggerEffectLogic_Hurt()
    {

    }
    public override void ExcuteAction(TriggerInfo triggerInfo, TriggerEffectInfo effectInfo)
    {
        //Debug.Log("TriggerGroupId = " + triggerInfo.triggerGroup.Id + "TriggerEffectLogic_Hurt");
        CharacterInfo charInfo = triggerInfo.charInfo;
        if (!charInfo.IsDead())
        {
            CharacterInfo targetInfo = charInfo.GetTargetInfo();
            if (targetInfo.charId == 50001)
            {
                //Debug.Log("骑士正在掉血" + targetInfo.GetAttr(CharAttr.Hp));
            }
            BattleUtils.CalcAtkDamage(charInfo, targetInfo);
            if (targetInfo.IsDead())
            {
                targetInfo.ChangeState("die");
            }
        }
        //Debug.Log(charInfo);
        //Debug.Log(charInfo.GetAttackInfo());
    }
}

