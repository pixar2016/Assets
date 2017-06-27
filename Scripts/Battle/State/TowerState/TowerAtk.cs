using System;
using System.Collections.Generic;
using UnityEngine;
public class TowerAtk : StateBase
{
    public AttackTowerInfo towerInfo;
    public float attackTime;
    public float curTime;
    public TowerAtk(AttackTowerInfo _towerInfo)
    {
        towerInfo = _towerInfo;
        attackTime = 0;
        curTime = 0;
    }

    public void SetParam(params object[] args)
    {

    }

    public void EnterExcute()
    {
        //towerInfo.StartSkill(towerInfo.attackSkill);
        //Debug.Log(towerInfo.GetFinalAttr(CharAttr.AttackTime));
        attackTime = 2;//towerInfo.GetFinalAttr(CharAttr.AttackTime);//towerInfo.attackTime;
        towerInfo.StartAttack();
        curTime = 0;
    }

    public void AttackEnd()
    {
        CharacterInfo attackCharInfo = towerInfo.GetTargetInfo();
        //若死亡或者超出了攻击范围，则回归待机重新寻找目标
        if (attackCharInfo.IsDead() || !WithinRange(towerInfo, attackCharInfo))
        {
            towerInfo.ChangeState("idle");
        }
        else
        {
            towerInfo.ChangeState("attack");
        }
    }

    public bool WithinRange(TowerInfo towerInfo, CharacterInfo target)
    {
        if (Vector3.Distance(towerInfo.GetPosition(), target.GetPosition()) <= 100)
            return true;
        else
            return false;
    }

    public void Excute()
    {
        curTime += Time.deltaTime;
        if (curTime >= attackTime)
        {
            AttackEnd();
            curTime = 0;
        }
    }

    public void ExitExcute()
    {
    }
}

