using UnityEngine;
using System.Collections.Generic;


public class SoliderInfo : CharacterInfo
{
    //攻击目标
    private CharacterInfo attackCharInfo;
    //兵营停留位置
    public Vector3 barrackSoliderPos;
    //出生位置
    public Vector3 bornPos;
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
    }
    public SoliderInfo(int soliderIndexId, CharacterPrototype charInfo)
    {
        charProto = charInfo;
        Id = soliderIndexId;
        charId = charInfo.charId;
        InitAttr(charInfo);
        InitStatusMachine();
        attackSkill = SkillManager.getInstance().AddSkill(1, this);
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
        SetAttr(CharAttr.Speed, 10);
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

    //关联兵营停留点和出生点，并把位置放到出生点
    public void SetBarrackPos(Vector3 staypos, Vector3 bornpos)
    {
        barrackSoliderPos = staypos;
        bornPos = bornpos;
        SetPosition(bornpos);
    }

    //设置攻击目标等信息
    public void SetAtkInfo(CharacterInfo charInfo)
    {
        attackCharInfo = charInfo;
    }

    public CharacterInfo GetTargetInfo()
    {
        return attackCharInfo;
    }

    public Vector3 GetBarrackPos()
    {
        return barrackSoliderPos;
    }

    public bool WithinStayPos()
    {
        if (BattleUtils.Distance2(barrackSoliderPos, GetPosition()) < 0.1f)
        {
            return true;
        }
        return false;
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
            //小型单位如果已经有兵种阻拦，则不会再被占用，大型单位可以有多个兵种阻拦
            if (monster.GetAtkInfo() == null && BattleUtils.Distance2(this.GetPosition(), monster.GetPosition()) <= 150
                && BattleUtils.Distance2(this.GetPosition(), barrackSoliderPos) <= 150)
            {
                return monster;
            }
        }
        return null;
    }

    public override void ChangeState(string _state, StateParam _param = null)
    {
        if (_state == "attack")
        {
            soliderStateMachine.ChangeState(soliderAtk, _param);
        }
        else if (_state == "die")
        {
            soliderStateMachine.ChangeState(soliderDead, _param);
        }
        else if (_state == "idle")
        {
            soliderStateMachine.ChangeState(soliderIdle, _param);
        }
        else if (_state == "move")
        {
            soliderStateMachine.ChangeState(soliderMove, _param);
        }
        else if(_state == "ready")
        {
            soliderStateMachine.ChangeState(soliderReady, _param);
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
            DoAction("run1");
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

    public override void StartAttack(CharacterInfo targetInfo)
    {
        SkillManager.getInstance().StartSkill(attackSkill, new TriggerData(targetInfo));
    }

    public float GetSpeed()
    {
        //return GetAttr(CharAttr.Speed) * (1 + GetAttr(CharAttr.SpeedPer));
        return GetFinalAttr(CharAttr.Speed);
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
        InitAttr(charProto);
        SetPosition(bornPos);
    }
}
