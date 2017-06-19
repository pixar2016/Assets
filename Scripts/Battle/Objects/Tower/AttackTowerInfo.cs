using System;
using System.Collections.Generic;
using UnityEngine;

//普通攻击类型塔 箭塔、魔塔、炮塔，因为状态机一致分配到一类中
public class AttackTowerInfo : TowerInfo
{
    
    public CharacterInfo attackCharInfo;
    //public int attackDamage;
    //public int attackSpeed;
    //public List<SkillInfo> attackSkillList;
    public SkillInfo attackSkill;

    public StateMachine towerStateMachine;
    public TowerAtk towerAtk;
    public TowerIdle towerIdle;
    public AttackTowerInfo(int indexId, int towerId) 
        : base(indexId, towerId)
    {
        towerStateMachine = new StateMachine();
        towerAtk = new TowerAtk(this);
        towerIdle = new TowerIdle(this);

        attackSkill = SkillManager.getInstance().AddSkill(this.towerData._attackId, this);
    }

    public AttackTowerInfo(int indexId, CharacterPrototype proto)
        : base(indexId, proto)
    {
        towerStateMachine = new StateMachine();
        towerAtk = new TowerAtk(this);
        towerIdle = new TowerIdle(this);

        attackSkill = SkillManager.getInstance().AddSkill(this.towerData._attackId, this);
    }

    public override void SetTargetInfo(CharacterInfo charInfo)
    {
        attackCharInfo = charInfo;
    }

    public override CharacterInfo GetTargetInfo()
    {
        return attackCharInfo;
    }

    public override void StartAttack()
    {
        SkillManager.getInstance().StartSkill(attackSkill);
    }

    MonsterInfo FindMonster()
    {
        List<MonsterInfo> monsterList = EntityManager.getInstance().GetMonsterInfo();
        foreach (MonsterInfo monster in monsterList)
        {
            if (Vector3.Distance(this.GetPosition(), monster.GetPosition()) <= 100)
            {
                return monster;
            } 
        }
        return null;
    }

    //是否在攻击范围内
    public bool WithinRange(CharacterInfo target)
    {
        if (Vector3.Distance(this.GetPosition(), target.GetPosition()) <= 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void ChangeState(string stateName, params object[] args)
    {
        if (stateName == "attack")
        {
            towerStateMachine.ChangeState(towerAtk, args);
        }
        else if (stateName == "idle")
        {
            towerStateMachine.ChangeState(towerIdle, args);
        }
    }

    public override void Update()
    {
        towerStateMachine.Excute();
    }
}

