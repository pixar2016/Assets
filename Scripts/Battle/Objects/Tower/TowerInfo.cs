using System;
using System.Collections.Generic;
using UnityEngine;

//基础塔类
public class TowerInfo : CharacterInfo
{
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
    }

    public TowerInfo(int indexId, CharacterPrototype proto)
    {
        this.Id = indexId;
        this.charId = proto.charId;
        this.towerData = J_Tower.GetData(charId);
        this.towerBase = towerData._towerBase;
        this.shooter = towerData._Shooter;
        this.towerType = towerData._towerType;
    }

    public virtual void ChangeState(string stateName, params object[] args)
    {

    }

    public virtual void Update()
    {

    }
}

