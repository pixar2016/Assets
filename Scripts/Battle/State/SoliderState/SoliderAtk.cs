using System;
using System.Collections.Generic;
using UnityEngine;

public class SoliderAtk : StateBase
{
    public SoliderInfo soliderInfo;
    //攻击目标
    public CharacterInfo attackInfo;
    public float attackTime;
    public float curTime;
    //普通攻击放到状态机里做，技能用SkillManager做，普通攻击不作为技能处理
    public SoliderAtk(SoliderInfo _soliderInfo)
    {
        soliderInfo = _soliderInfo;
        attackTime = 0;
        curTime = 0;
    }

    public void SetParam(StateParam _param)
    {
        if (_param == null)
        {
            return;
        }
        attackInfo = _param.targetInfo;
    }

    public void EnterExcute()
    {
        //soliderInfo.StartSkill(soliderInfo.attackSkill);
        attackTime = soliderInfo.GetFinalAttr(CharAttr.AttackTime);
        soliderInfo.SetAtkInfo(attackInfo);
        soliderInfo.StartAttack(attackInfo);
        curTime = 0;
    }

    public void AttackEnd()
    {
        //Debug.Log("AttackEnd");
        //CharacterInfo attackCharInfo = soliderInfo.GetTargetInfo();
        //如果目标死亡，回归空闲状态
        if (attackInfo == null || attackInfo.IsDead())
        {
            soliderInfo.ChangeState("idle");
        }
        //如果目标未死亡，继续攻击状态
        else
        {
            //Debug.Log("continue attack");
            soliderInfo.ChangeState("attack", new StateParam(attackInfo));
        }
    }

    public void Excute()
    {
        curTime += Time.deltaTime;
        //若完成一次攻击
        if (curTime >= attackTime)
        {
            //执行普通攻击计算伤害方法
            //若目标为空或者已死亡，返回停留位置
            //否则，继续攻击
            //可以在这里添加攻击后被动技能
            AttackEnd();
        }
    }

    public void ExitExcute()
    {
    }
}

