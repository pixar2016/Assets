﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using EventDispatcherSpace;

public enum CharAttr
{
    //血量
    Hp,
    //血量增加百分比
    HpPer,
    //血量上限
    HpMax,
    //血量上限增加百分比
    HpMaxPer,
    //攻击时间
    AttackTime,
    //攻击速度
    AttackSpeed,
    //攻击速度增加百分比
    AttackSpeedPer,
    //攻击伤害
    AttackDamage,
    //攻击伤害增加百分比
    AttackDamagePer,
    //护甲类型
    ArmorType,
    //速度
    Speed,
    //速度增加百分比
    SpeedPer
}

public class CharacterInfo
{
    //序列ID
    public int Id;
    public int charId;
    public string charName;
    public Vector3 position;
    public Vector3 rotation;
    public string actionName;
    //key-CharAttr value-attrValue
    public Dictionary<int, int> attrList;
    //用于广播事件
    public MiniEventDispatcher eventDispatcher;
    //兵种模板
    public CharacterPrototype charProto;
    //标记 0-正常 1-添加 -1-删除
    public int dirtySign;
    public CharacterInfo()
    {
        eventDispatcher = new MiniEventDispatcher();
        position = new Vector3(0, 0, 0);
        rotation = Vector3.zero;
        attrList = new Dictionary<int, int>();
        dirtySign = 0;
    }

    public virtual void SetPosition(float x, float y, float z)
    {
        //position = new Vector3(x, y, z);
        position.x = x;
        position.y = y;
        position.z = y / 20;
    }

    public virtual void SetPosition(Vector3 _pos)
    {
        _pos.z = _pos.y / 20f;
        position = _pos;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public virtual Vector3 GetBulletPos()
    {
        return position;
    }

    public virtual void SetRotation(float x, float y, float z)
    {
        rotation = new Vector3(x, y, z);
    }

    public Vector3 GetRotation()
    {
        return rotation;
    }

    public virtual bool IsDead()
    {
        return false;
    }

    public void DoAction(string actionName, float actionTime = 0)
    {
        //Debug.Log("Dispatching: SampleEvent " + sampleEvent);
        this.actionName = actionName;
        this.eventDispatcher.Broadcast("DoAction", actionName, actionTime);
    }

    public virtual void ChangeState(string _state, StateParam _param = null)
    {

    }

    public virtual void SetState(string _state)
    {

    }

    //开始一个技能
    public void StartSkill(SkillInfo skillInfo)
    {
        SkillManager.getInstance().StartSkill(skillInfo);
    }
    //开始普通攻击
    public virtual void StartAttack(CharacterInfo targetInfo){}

    public virtual void Run(Vector3 targetPos){}

    //得到某一个属性基础值
    public virtual int GetAttr(CharAttr attrName)
    {
        int temp = (int)attrName;
        if (attrList.ContainsKey(temp))
        {
            return attrList[temp];
        }
        else
        {
            return -1;
        }
    }
    

    //对目标造成普通攻击伤害
    public virtual void Hurt()
    {
    }
    //得到某一个属性的最终值
    public virtual float GetFinalAttr(CharAttr attrName)
    {
        switch (attrName)
        {
            case CharAttr.Hp:
                return GetAttr(CharAttr.Hp) * (1 + GetAttr(CharAttr.HpPer));
            case CharAttr.HpMax:
                return GetAttr(CharAttr.HpMax) * (1 + GetAttr(CharAttr.HpMaxPer));
            case CharAttr.AttackTime:
                int attackSpeed = GetAttr(CharAttr.AttackSpeed);
                return (attackSpeed == 0) ? 0 : (1.0f / (attackSpeed * (1 + GetAttr(CharAttr.AttackSpeedPer))));
            case CharAttr.AttackSpeed:
                return GetAttr(CharAttr.AttackSpeed) * (1 + GetAttr(CharAttr.AttackSpeedPer));
            case CharAttr.AttackDamage:
                return GetAttr(CharAttr.AttackDamage) * (1 + GetAttr(CharAttr.AttackDamagePer));
            case CharAttr.Speed:
                return GetAttr(CharAttr.Speed) * (1 + GetAttr(CharAttr.SpeedPer));
            default:
                break;
        }
        return GetAttr(attrName);
    }
    //改变某个属性
    public virtual bool ChangeAttr(CharAttr attrName, int changeNum)
    {
        int temp = (int)attrName;
        if (attrList.ContainsKey(temp))
        {
            attrList[temp] += changeNum;
            return true;
        }
        else
        {
            return false;
        }
    }

    //设置某个属性值
    public void SetAttr(CharAttr attrName, int attrNum)
    {
        int temp = (int)attrName;
        if (attrList.ContainsKey(temp))
        {
            attrList[temp] = attrNum;
        }
        else
        {
            attrList.Add(temp, attrNum);
        }
    }

    //设置标记，false-添加 true-移除
    public void SetDirtySign(bool isDirty)
    {
        if (isDirty)
        {
            dirtySign--;
        }
        else
        {
            dirtySign++;
        }
    }

    public virtual void Release()
    {

    }
}
