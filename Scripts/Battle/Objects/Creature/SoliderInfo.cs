using UnityEngine;
using System.Collections.Generic;


public class SoliderInfo : CharacterInfo
{
    //攻击目标
    private CharacterInfo attackCharInfo;
    //兵营停留位置
    public Vector3 barrackSoliderPos;

    //宽度
    public float width;
    //高度
    public float height;

    public StateMachine soliderStateMachine;
    public SoliderAtk soliderAtk;
    public SoliderDead soliderDead;
    public SoliderIdle soliderIdle;
    public SoliderMove soliderMove;
    public SoliderReady soliderReady;
    public SkillInfo attackSkill;

    public SoliderInfo(int soliderIndexId, int soliderId)
    {
        Id = soliderIndexId;
        charId = soliderId;
        InitAttr(charId);
        InitStatusMachine();
        attackSkill = SkillManager.getInstance().AddSkill(1, this);
        attackTime = AnimationCache.getInstance().getAnimation(charName).getMeshAnimation("attack").getAnimTime();
    }
    public SoliderInfo(int soliderIndexId, CharacterPrototype charInfo)
    {
        Id = soliderIndexId;
        charId = charInfo.charId;
        InitAttr(charInfo);
        InitStatusMachine();
        attackSkill = SkillManager.getInstance().AddSkill(1, this);
        attackTime = charInfo.attackTime;
        charInfo.eventDispatcher.Register("ChangeProtoAttr", ChangeProtoAttr);
    }

    public void ChangeProtoAttr(object[] param)
    {

    }
    public void InitStatusMachine()
    {
        soliderStateMachine = new StateMachine();
        soliderAtk = new SoliderAtk(this);
        soliderDead = new SoliderDead(this);
        soliderIdle = new SoliderIdle(this);
        soliderMove = new SoliderMove(this);
        soliderReady = new SoliderReady(this);
    }

    public void InitAttr(int _charId)
    {
        D_Creature creatureData = J_Creature.GetData(_charId);
        charName = creatureData._modelName;
        if (charName == null)
        {
            Debug.LogError("SoliderModelName" + _charId + " is NULL");
        }
        SetAttr(CharAttr.HpMax, creatureData._hp);
        SetAttr(CharAttr.HpMaxPer, 0);
        SetAttr(CharAttr.Hp, creatureData._hp);
        SetAttr(CharAttr.HpPer, 0);
        SetAttr(CharAttr.AttackSpeed, creatureData._attackSpeed);
        SetAttr(CharAttr.AttackSpeedPer, 0);
        SetAttr(CharAttr.AttackDamage, creatureData._attackDamage);
        SetAttr(CharAttr.AttackDamagePer, 0);
        SetAttr(CharAttr.ArmorType, creatureData._defenceType);
        SetAttr(CharAttr.Speed, 60);
        SetAttr(CharAttr.SpeedPer, 0);
    }

    public void InitAttr(CharacterPrototype _charInfo)
    {
        charName = _charInfo.charName;
        SetAttr(CharAttr.HpMax, _charInfo.GetAttr(CharAttr.HpMax));
        SetAttr(CharAttr.HpMaxPer, _charInfo.GetAttr(CharAttr.HpMaxPer));
        SetAttr(CharAttr.Hp, _charInfo.GetAttr(CharAttr.Hp));
        SetAttr(CharAttr.HpPer, _charInfo.GetAttr(CharAttr.HpPer));
        SetAttr(CharAttr.AttackSpeed, _charInfo.GetAttr(CharAttr.AttackSpeed));
        SetAttr(CharAttr.AttackSpeedPer, _charInfo.GetAttr(CharAttr.AttackSpeedPer));
        SetAttr(CharAttr.AttackDamage, _charInfo.GetAttr(CharAttr.AttackDamage));
        SetAttr(CharAttr.AttackDamagePer, _charInfo.GetAttr(CharAttr.AttackDamagePer));
        SetAttr(CharAttr.ArmorType, _charInfo.GetAttr(CharAttr.ArmorType));
        SetAttr(CharAttr.Speed, _charInfo.GetAttr(CharAttr.Speed));
        SetAttr(CharAttr.SpeedPer, _charInfo.GetAttr(CharAttr.SpeedPer));
    }

    //关联兵营停留点
    public void SetBarrackPos(Vector3 pos)
    {
        barrackSoliderPos = pos;
    }

    //设置攻击目标等信息
    public override void SetTargetInfo(CharacterInfo charInfo)
    {
        attackCharInfo = charInfo;
    }

    public override CharacterInfo GetTargetInfo()
    {
        return attackCharInfo;
    }
    public Vector3 GetAttackMovePos()
    {
        Vector3 atkPos = attackCharInfo.GetPosition();
        Vector3 movePos;
        Vector3 atkRot = attackCharInfo.GetRotation();
        //攻击目标向左
        if (atkRot.y > 0)
        {
            movePos = atkPos - new Vector3(30f, 0, 0); 
        }
        //攻击目标向右
        else
        {
            movePos = atkPos + new Vector3(30f, 0, 0);
        }
        return movePos;
    }
    public Vector3 GetBarrackPos()
    {
        return barrackSoliderPos;
    }

    //执行AI
    public CharacterInfo RunAI()
    {
        //Debug.Log("RunAI");
        return FindMonster();
    }
    //寻找怪物
    MonsterInfo FindMonster()
    {
        List<MonsterInfo> monsterList = EntityManager.getInstance().GetMonsterInfo();
        foreach (MonsterInfo monster in monsterList)
        {
            if (monster.GetTargetInfo() == null && Vector3.Distance(this.GetPosition(), monster.GetPosition()) <= 150)
            {
                return monster;
            }
        }
        return null;
    }

    public override void ChangeState(string _state, params object[] args)
    {
        if (_state == "attack")
        {
            soliderStateMachine.ChangeState(soliderAtk, args);
        }
        else if (_state == "die")
        {
            soliderStateMachine.ChangeState(soliderDead, args);
        }
        else if (_state == "idle")
        {
            soliderStateMachine.ChangeState(soliderIdle, args);
        }
        else if (_state == "move")
        {
            soliderStateMachine.ChangeState(soliderMove, args);
        }
        else if(_state == "ready")
        {
            soliderStateMachine.ChangeState(soliderReady, args);
        }
    }

    public override void SetState(string _state)
    {
        if (_state == "attack")
        {
            soliderStateMachine.SetCurrentState(soliderAtk);
        }
        else if (_state == "die")
        {
            soliderStateMachine.SetCurrentState(soliderDead);
        }
        else if (_state == "idle")
        {
            soliderStateMachine.SetCurrentState(soliderIdle);
        }
        else if (_state == "move")
        {
            soliderStateMachine.SetCurrentState(soliderMove);
        }
        else if (_state == "ready")
        {
            soliderStateMachine.SetCurrentState(soliderReady);
        }
    }

    public override void Run(Vector3 targetPos)
    {
        Vector3 curPos = this.GetPosition();
        if (targetPos.y > curPos.y && Mathf.Abs(targetPos.y - curPos.y) > Mathf.Abs(targetPos.x - curPos.x))
        {
            SetRotation(0, 0, 0);
            DoAction("run2");
        }
        else if (targetPos.x >= curPos.x)
        {
            SetRotation(0, 0, 0);
            DoAction("run1");
        }
        else
        {
            SetRotation(0, 180, 0);
            DoAction("run1");
        }
    }

    public float GetSpeed()
    {
        return GetAttr(CharAttr.Speed) * (1 + GetAttr(CharAttr.SpeedPer));
    }

    public override bool IsDead()
    {
        return GetAttr(CharAttr.Hp) <= 0;
    }

    public void Update()
    {
        soliderStateMachine.Excute();
    }

    public override void Release()
    {
        
    }
    //重置信息，重置血量等信息
    public void Reset()
    {

    }
}
