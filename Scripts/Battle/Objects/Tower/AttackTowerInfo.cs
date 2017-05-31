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

        attackSkill = SkillManager.getInstance().AddSkill(2, this);
        attackTime = 1;
        //若有弓手，攻击时长为弓手攻击动作
        //if (this.shooter != null)
        //{
        //    attackTime = AnimationCache.getInstance().getAnimation(this.shooter).getMeshAnimation("attack").getAnimTime();
        //}
        //若没有弓手，攻击时长为塔身攻击动作
        //else
        //{
        //    attackTime = AnimationCache.getInstance().getAnimation(this.towerBase).getMeshAnimation("attack").getAnimTime();
        //}
    }

    public AttackTowerInfo(int indexId, CharacterPrototype proto)
        : base(indexId, proto)
    {
        towerStateMachine = new StateMachine();
        towerAtk = new TowerAtk(this);
        towerIdle = new TowerIdle(this);

        attackSkill = SkillManager.getInstance().AddSkill(2, this);
        attackTime = 1;
    }

    public override void SetTargetInfo(CharacterInfo charInfo)
    {
        attackCharInfo = charInfo;
    }

    public override CharacterInfo GetTargetInfo()
    {
        return attackCharInfo;
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

