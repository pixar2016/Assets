﻿using System;
using System.Collections.Generic;
using UnityEngine;

//单位原型类，主要用于针对某类对象的属性的改变，在改变属性后，需要通知已创建的对象改变属性
//举个例子，某个技能树提高所有兵种的血量上限，但如果进入战场，血量上限仍然读表赋值，不正确，但对每个兵种释放一次该技能，增加不必要的消耗和复杂性。所以可以修改原型来解决。
public class CharacterPrototype
{
    public int charId;
    public string charName;
    public Dictionary<int, int> attrList;
    public int attackTime;
    public MiniEventDispatcher eventDispatcher;

    public CharacterPrototype(int _charId)
    {
        charId = _charId;
        InitAttr(charId);
        eventDispatcher = new MiniEventDispatcher();
        attrList = new Dictionary<int, int>();
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
    //得到某一个属性值
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
    //改变某个属性
    public virtual bool ChangeAttr(CharAttr attrName, int changeNum)
    {
        int temp = (int)attrName;
        if (attrList.ContainsKey(temp))
        {
            attrList[temp] += changeNum;
            this.eventDispatcher.Broadcast("ChangeProtoAttr", attrName, changeNum);
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

    //克隆原型类，分配indexId和怪物行走的路线
    public MonsterInfo CloneMonster(int _monsterIndex, PathInfo _pathInfo)
    {
        return new MonsterInfo(_monsterIndex, this, _pathInfo);
    }

    //public SoliderInfo CloneSolider()
    //{
    //    return new SoliderInfo();
    //}
}
