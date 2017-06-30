using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAtk : StateBase
{
    public MonsterInfo monsterInfo;
    public CharacterInfo attackInfo;
    public float attackTime;
    public float curTime;
    public CreatureAtk(MonsterInfo _monsterInfo)
    {
        monsterInfo = _monsterInfo;
        attackTime = 0;
        curTime = 0;
    }

    public void SetParam(StateParam _param)
    {
        if (_param == null)
            return;
        attackInfo = _param.targetInfo;
    }

    public void EnterExcute()
    {
        //Debug.Log("CreatureAtk EnterExcute");
        //monsterInfo.StartSkill(monsterInfo.attackSkill);
        attackTime = monsterInfo.GetFinalAttr(CharAttr.AttackTime);
        monsterInfo.StartAttack(attackInfo);
        curTime = 0;
    }

    public void AttackEnd()
    {
        //CharacterInfo attackCharInfo = monsterInfo.GetTargetInfo();
        if (attackInfo == null)
        {
            monsterInfo.ChangeState("idle");
        }
        else if (attackInfo.IsDead())
        {
            attackInfo.ChangeState("die");
            monsterInfo.ChangeState("move");
        }
        else
        {
            //Debug.Log("continue attack");
            monsterInfo.ChangeState("attack", new StateParam(attackInfo));
        }
    }

    public void Excute()
    {
        curTime += Time.deltaTime;
        if (curTime >= attackTime)
        {
            //执行普通攻击计算伤害方法
            //若目标为空或者已死亡，继续行动
            //否则，继续攻击
            //可以在这里添加攻击后被动技能
            AttackEnd();
        }
    }

    public void ExitExcute()
    {
    }
}

