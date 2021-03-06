﻿using UnityEngine;
using System.Collections.Generic;


public class MonsterInfo : CharacterInfo
{
    public MonsterView charView;
    //攻击目标
    private CharacterInfo attackCharInfo;
    //攻击需要移动的位置
    private Vector3 attackMovePos;
    //状态机
    public StateMachine creatureStateMachine;
    public CreatureAtk creatureAtk;
    public CreatureDead creatureDead;
    public CreatureIdle creatureIdle;
    public CreatureMove creatureMove;
    //普通攻击
    public SkillInfo attackSkill;
    //当前怪物行动路径
    public PathInfo pathInfo;
    //当前走到路径第几个点
    public int curPathNum;
    //正常初始化
    public MonsterInfo(int creatureIndexId, int creatureId, PathInfo _pathInfo)
    {
        Id = creatureIndexId;
        charId = creatureId;
        InitAttr(charId);
        InitStatusMachine();
        attackSkill = SkillManager.getInstance().AddSkill(1, this);
        pathInfo = _pathInfo;
        curPathNum = 0;
        position = pathInfo.GetPoint(curPathNum);
    }
    //复制原型类中的数据
    public MonsterInfo(int creatureIndexId, CharacterPrototype charInfo, PathInfo _pathInfo)
    {
        Id = creatureIndexId;
        charId = charInfo.charId;
        InitAttr(charInfo);
        InitStatusMachine();
        attackSkill = SkillManager.getInstance().AddSkill(1, this);
        pathInfo = _pathInfo;
        curPathNum = 0;
        position = pathInfo.GetPoint(curPathNum);
        charInfo.eventDispatcher.Register("ChangeProtoAttr", ChangeProtoAttr);
    }

    public void ChangeProtoAttr(object[] param)
    {
        CharAttr attrName = (CharAttr)param[1];
        int attrNum = (int)param[2];
        ChangeAttr(attrName, attrNum);
    }

    public void InitStatusMachine()
    {
        creatureStateMachine = new StateMachine();
        creatureAtk = new CreatureAtk(this);
        creatureDead = new CreatureDead(this);
        creatureIdle = new CreatureIdle(this);
        creatureMove = new CreatureMove(this);
    }

    public void InitAttr(int _charId)
    {
        D_Creature creatureData = J_Creature.GetData(_charId);
        charName = creatureData._modelName;
        if (charName == null)
        {
            Debug.LogError("MonsterModelName" + _charId + " is NULL");
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
    //复制charInfo的属性值
    public void InitAttr(CharacterPrototype _charInfo)
    {
        charProto = _charInfo;
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

    public override void SetPosition(float x, float y, float z)
    {
        base.SetPosition(x, y, z);
        charView.SetPosition(this.position);
    }

    public override void SetPosition(Vector3 _pos)
    {
        base.SetPosition(_pos);
        charView.SetPosition(this.position);
    }

    public override void SetRotation(float x, float y, float z)
    {
        base.SetRotation(x, y, z);
        charView.SetRotation(this.rotation);
    }

    public bool ReachNextPoint()
    {
        if (curPathNum + 1 >= pathInfo.GetCount())
        {
            return false;
        }
        curPathNum++;
        return true;
    }
    public Vector3 GetNextPoint()
    {
        if (curPathNum + 1 >= pathInfo.GetCount())
        {
            return this.GetPosition();
        }
        return pathInfo.GetPoint(curPathNum + 1);
    }
    ////设置攻击目标等信息
    public void SetAtkInfo(CharacterInfo charInfo)
    {
        attackCharInfo = charInfo;
    }

    public CharacterInfo GetAtkInfo()
    {
        return attackCharInfo;
    }

    public override void ChangeState(string _state, StateParam _param = null)
    {
        if (_state == "attack")
        {
            creatureStateMachine.ChangeState(creatureAtk, _param);
        }
        else if (_state == "die")
        {
            creatureStateMachine.ChangeState(creatureDead, _param);
        }
        else if (_state == "idle")
        {
            creatureStateMachine.ChangeState(creatureIdle, _param);
        }
        else if (_state == "move")
        {
            creatureStateMachine.ChangeState(creatureMove, _param);
        }
    }

    public override void SetState(string _state)
    {
        if (_state == "attack")
        {
            creatureStateMachine.SetCurrentState(creatureAtk);
        }
        else if (_state == "die")
        {
            creatureStateMachine.SetCurrentState(creatureDead);
        }
        else if (_state == "idle")
        {
            creatureStateMachine.SetCurrentState(creatureIdle);
        }
        else if (_state == "move")
        {
            creatureStateMachine.SetCurrentState(creatureMove);
        }
    }

    //得到当前状态类的名字
    public string GetCurrentState()
    {
        if (creatureStateMachine.GetCurrentState() == null)
        {
            return "";
        }
        return creatureStateMachine.GetCurrentState().ToString();
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

    public override void StartAttack(CharacterInfo targetInfo)
    {
        SkillManager.getInstance().StartSkill(attackSkill, new TriggerData(targetInfo));
    }

    public float GetSpeed()
    {
        return GetFinalAttr(CharAttr.Speed);
    }

    public override bool IsDead()
    {
        return GetAttr(CharAttr.Hp) <= 0;
    }

    public void Update()
    {
        creatureStateMachine.Excute();
    }
}
