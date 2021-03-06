﻿using System;
using System.Collections.Generic;
using UnityEngine;

//基础塔类
public class TowerInfo : CharacterInfo
{
    public TowerView towerView;
    public D_Tower towerData;
    public string towerBase;
    //没有就为空
    public string shooter;
    public int towerType;

    public TowerInfo()
    {

    }

    public TowerInfo(int indexId, int towerId)
    {
        this.Id = indexId;
        this.charId = towerId;
        this.towerData = J_Tower.GetData(towerId);
        this.towerBase = towerData._towerBase;
        this.shooter = towerData._Shooter;
        this.towerType = towerData._towerType;
        InitAttr(towerId);
    }

    public TowerInfo(int indexId, CharacterPrototype proto)
    {
        this.Id = indexId;
        this.charId = proto.charId;
        this.towerData = J_Tower.GetData(charId);
        this.towerBase = towerData._towerBase;
        this.shooter = towerData._Shooter;
        this.towerType = towerData._towerType;
        InitAttr(proto);
    }

    public void InitAttr(int _charId)
    {
        charName = towerData._name;
        SetAttr(CharAttr.AttackSpeed, towerData._attackSpeed);
        SetAttr(CharAttr.AttackSpeedPer, 0);
        SetAttr(CharAttr.AttackDamage, towerData._attackDamage);
        SetAttr(CharAttr.AttackDamagePer, 0);
    }

    public void InitAttr(CharacterPrototype _charInfo)
    {
        charName = _charInfo.charName;
        SetAttr(CharAttr.AttackSpeed, _charInfo.GetAttr(CharAttr.AttackSpeed));
        SetAttr(CharAttr.AttackSpeedPer, _charInfo.GetAttr(CharAttr.AttackSpeedPer));
        SetAttr(CharAttr.AttackDamage, _charInfo.GetAttr(CharAttr.AttackDamage));
        SetAttr(CharAttr.AttackDamagePer, _charInfo.GetAttr(CharAttr.AttackDamagePer));
    }

    public void InitAttr()
    {

    }

    public override void SetPosition(float x, float y, float z)
    {
        //position = new Vector3(x, y, z);
        base.SetPosition(x, y, z);
        towerView.SetPosition(this.position);
    }

    public override void SetPosition(Vector3 _pos)
    {
        base.SetPosition(_pos);
        towerView.SetPosition(this.position);
    }

    public override Vector3 GetBulletPos()
    {
        return towerView.GetBulletPos();
    }

    public virtual void ChangeState(string stateName, StateParam _param = null)
    {

    }

    public virtual void Update()
    {

    }
}

