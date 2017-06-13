using UnityEngine;
using System.Collections.Generic;


public class MonsterInfo : CharacterInfo
{
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
        SetAttr(CharAttr.Speed, 60);
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
    //设置攻击目标等信息
    public override void SetTargetInfo(CharacterInfo charInfo)
    {
        attackCharInfo = charInfo;
    }

    public override CharacterInfo GetTargetInfo()
    {
        return attackCharInfo;
    }

    public override void ChangeState(string _state, params object[] args)
    {
        if (_state == "attack")
        {
            creatureStateMachine.ChangeState(creatureAtk, args);
        }
        else if (_state == "die")
        {
            creatureStateMachine.ChangeState(creatureDead, args);
        }
        else if (_state == "idle")
        {
            creatureStateMachine.ChangeState(creatureIdle, args);
        }
        else if (_state == "move")
        {
            creatureStateMachine.ChangeState(creatureMove, args);
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
    ////向上走
    //public override void RunUp()
    //{
    //    SetRotation(0, 0, 0);
    //    DoAction("run2");
    //}
    ////向下走
    //public override void RunDown()
    //{
    //    SetRotation(0, 0, 180);
    //    DoAction("run2");
    //}
    ////向右走
    //public override void RunRight()
    //{
    //    SetRotation(0, 0, 0);
    //    DoAction("run1");
    //}
    ////向左走
    //public override void RunLeft()
    //{
    //    SetRotation(0, 180, 0);
    //    DoAction("run1");
    //}
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
